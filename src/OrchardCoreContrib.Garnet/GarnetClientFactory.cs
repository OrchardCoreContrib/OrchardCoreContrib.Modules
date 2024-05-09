using Garnet.client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a factory for creating instances of <see cref="GarnetClient"/>.
/// </summary>
public class GarnetClientFactory : IGarnetClientFactory, IDisposable
{
    private static readonly ConcurrentDictionary<string, Lazy<Task<GarnetClient>>> _factories = new();
    
    private static volatile int _registered;
    private static volatile int _refCount;

    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger _logger;

    /// <summary>
    /// Creates a new instance of <see cref="GarnetClientFactory"/>.
    /// </summary>
    /// <param name="lifetime">The <see cref="IHostApplicationLifetime"/>.</param>
    /// <param name="logger">The <see cref="ILogger{GarnetClientFactory}"/>.</param>
    public GarnetClientFactory(IHostApplicationLifetime lifetime, ILogger<GarnetClientFactory> logger)
    {
        Interlocked.Increment(ref _refCount);

        _lifetime = lifetime;
        
        if (Interlocked.CompareExchange(ref _registered, 1, 0) == 0)
        {
            _lifetime.ApplicationStopped.Register(Release);
        }

        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<GarnetClient> CreateAsync(GarnetOptions options)
    {
        var key = $"garnet://{options.Host}:{options.Port};username={options.UserName};password={options.Password}";
        
        if (_factories.TryGetValue(key, out var value))
        {
            var client = await value.Value;
            if (client is null)
            {
                _factories.Remove(key, out _);
            }
        }

        return await _factories.GetOrAdd(key, new Lazy<Task<GarnetClient>>(async () =>
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Creating a new instance of '{name}'. A single instance per configuration should be created across tenants. Total instances prior creating is '{count}'.", nameof(GarnetClient), _factories.Count);
                }

                var client = new GarnetClient(options.Host, options.Port, authUsername: options.UserName, authPassword: options.Password);

                await client.ConnectAsync();

                return client;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to connect to Garnet.");

                return null;
            }
        })).Value;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Interlocked.Decrement(ref _refCount) == 0 && _lifetime.ApplicationStopped.IsCancellationRequested)
        {
            Release();
        }
    }

    private static void Release()
    {
        if (Interlocked.CompareExchange(ref _refCount, 0, 0) == 0)
        {
            var factories = _factories.Values.ToArray();

            _factories.Clear();

            foreach (var factory in factories)
            {
                var client = factory.Value;
                client.Dispose();
            }
        }
    }
}
