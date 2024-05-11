using Garnet.client;
using Microsoft.Extensions.Caching.Distributed;
using OrchardCoreContrib.Garnet.Services;
using OrchardCoreContrib.Infrastructure;
using System.Text;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a distributed cache implementation using Garnet.
/// </summary>
public class GarnetCache : IDistributedCache
{
    private static readonly Action<long, string> _callback = (_, _) => { };

    private readonly GarnetClient _client;

    /// <summary>
    /// Creates a new instance of <see cref="GarnetCache"/>.
    /// </summary>
    /// <param name="garnetService">The <see cref="IGarnetService"/>.</param>
    public GarnetCache(IGarnetService garnetService)
    {
        _client = garnetService.Client;

        if (_client is null)
        {
            garnetService.ConnectAsync().GetAwaiter().GetResult();
            _client = garnetService.Client;
        }
    }

    /// <inheritdoc/>
    public byte[] Get(string key) => GetAsync(key).GetAwaiter().GetResult();

    /// <inheritdoc/>
    public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
    {
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));

        var value = await _client.StringGetAsync(key, token);

        if (value is null)
        {
            return [];
        }

        return Encoding.UTF8.GetBytes(value);
    }

    /// <inheritdoc/>
    public void Refresh(string key) => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task RefreshAsync(string key, CancellationToken token = default) => throw new NotImplementedException();

    /// <inheritdoc/>
    public void Remove(string key)
    {
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));

        _client.KeyDelete(key, _callback);
    }

    /// <inheritdoc/>
    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));

        await _client.KeyDeleteAsync(key, token);
    }

    /// <inheritdoc/>
    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));
        Guard.ArgumentNotNull(value, nameof(value));

        _client.StringSet(key, Encoding.UTF8.GetString(value), _callback);
    }

    /// <inheritdoc/>
    public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));
        Guard.ArgumentNotNull(value, nameof(value));

        await _client.StringSetAsync(key, Encoding.UTF8.GetString(value), token);
    }
}
