using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCore.Locking;
using OrchardCore.Locking.Distributed;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net;

namespace OrchardCoreContrib.Garnet.Services;

/// <summary>
/// Represents a distributed lock implementation based on Garnet service.
/// </summary>
/// <param name="garnetService">The <see cref="IGarnetService"/>.</param>
/// <param name="garnetOptions">The <see cref="IOptions{GarnetOptions}"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="logger">The <see cref="ILogger{GarnetLock}"/>.</param>
public class GarnetLock(
    IGarnetService garnetService,
    IOptions<GarnetOptions> garnetOptions,
    ShellSettings shellSettings,
    ILogger<GarnetLock> logger) : IDistributedLock
{
    private static readonly double _baseDelay = 100;
    private static readonly double _maxDelay = 10000;

    private readonly GarnetOptions _garnetOptions = garnetOptions.Value;
    private readonly string _hostName = Dns.GetHostName() + ':' + Environment.ProcessId;
    private readonly string _prefix = garnetService.InstancePrefix + shellSettings.Name + ':';

    /// <summary>
    /// Waits indefinitely until acquiring a named lock with a given expiration for the current tenant
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="expiration">The expiration time for the lock.</param>
    public async Task<ILocker> AcquireLockAsync(string key, TimeSpan? expiration = null)
        => (await TryAcquireLockAsync(key, TimeSpan.MaxValue, expiration)).locker;

    /// <summary>
    /// Tries to acquire a named lock in a given timeout with a given expiration for the current tenant.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="timeout">The timeout for acquiring the lock.</param>
    /// <param name="expiration">The expiration time for the lock.</param>
    /// <returns></returns>
    public async Task<(ILocker locker, bool locked)> TryAcquireLockAsync(string key, TimeSpan timeout, TimeSpan? expiration = null)
    {
        using (var cts = new CancellationTokenSource(timeout != TimeSpan.MaxValue ? timeout : Timeout.InfiniteTimeSpan))
        {
            var retries = 0.0;

            while (!cts.IsCancellationRequested)
            {
                var locked = await LockAsync(key, expiration ?? TimeSpan.MaxValue);

                if (locked)
                {
                    return (new Locker(this, key), locked);
                }

                try
                {
                    await Task.Delay(GetDelay(++retries), cts.Token);
                }
                catch (TaskCanceledException)
                {
                    if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("Timeout elapsed before acquiring the named lock '{LockName}' after the given timeout of '{Timeout}'.",
                            _prefix + key, timeout.ToString());
                    }
                }
            }
        }

        return (null, false);
    }

    public async Task<bool> IsLockAcquiredAsync(string key)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();

            if (garnetService.Client == null)
            {
                logger.LogError("Fails to check whether the named lock '{LockName}' is already acquired.", _prefix + key);

                return false;
            }
        }

        try
        {
            var database = (await ConnectionMultiplexer
                .ConnectAsync(GetConfigurationOptions(_garnetOptions)))
                .GetDatabase();
            
            return (await database.LockQueryAsync(_prefix + key)).HasValue;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to check whether the named lock '{LockName}' is already acquired.", _prefix + key);
        }

        return false;
    }

    private async Task<bool> LockAsync(string key, TimeSpan expiry)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();

            if (garnetService.Client == null)
            {
                logger.LogError("Fails to acquire the named lock '{LockName}'.", _prefix + key);
                
                return false;
            }
        }

        try
        {
            var database = (await ConnectionMultiplexer
                .ConnectAsync(GetConfigurationOptions(_garnetOptions)))
                .GetDatabase();

            return await database.LockTakeAsync(_prefix + key, _hostName, expiry);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to acquire the named lock '{LockName}'.", _prefix + key);
        }

        return false;
    }

    private async ValueTask ReleaseAsync(string key)
    {
        try
        {
            var database = (await ConnectionMultiplexer
               .ConnectAsync(GetConfigurationOptions(_garnetOptions)))
               .GetDatabase();

            await database.LockReleaseAsync(_prefix + key, _hostName);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to release the named lock '{LockName}'.", _prefix + key);
        }
    }

    private void Release(string key)
    {
        try
        {
            var database = ConnectionMultiplexer
               .ConnectAsync(GetConfigurationOptions(_garnetOptions))
               .GetAwaiter()
               .GetResult()
               .GetDatabase();

            database.LockRelease(_prefix + key, _hostName);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to release the named lock '{LockName}'.", _prefix + key);
        }
    }

    private sealed class Locker(GarnetLock garnetLock, string key) : ILocker
    {
        private bool _disposed;

        public ValueTask DisposeAsync()
        {
            if (_disposed)
            {
                return default;
            }

            _disposed = true;

            return garnetLock.ReleaseAsync(key);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            garnetLock.Release(key);
        }
    }

    private static TimeSpan GetDelay(double retries)
    {
        var delay = _baseDelay * (1.0 + ((Math.Pow(1.8, retries - 1.0) - 1.0) * (0.6 + new Random().NextDouble() * 0.4)));

        return TimeSpan.FromMilliseconds(Math.Min(delay, _maxDelay));
    }

    // TODO: Use explicit conversion operators to convert between GarnetOptions and ConfigurationOptions
    private static ConfigurationOptions GetConfigurationOptions(GarnetOptions garnetOptions)
    {
        var endPoints = new EndPointCollection
        {
            new DnsEndPoint(garnetOptions.Host, garnetOptions.Port)
        };
        var configOptions = new ConfigurationOptions
        {
            EndPoints = endPoints,
            ConnectTimeout = (int)TimeSpan.FromSeconds(2).TotalMilliseconds,
            SyncTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds,
            AsyncTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds,
            ReconnectRetryPolicy = new LinearRetry((int)TimeSpan.FromSeconds(10).TotalMilliseconds),
            ConnectRetry = 5,
            IncludeDetailInExceptions = true,
            AbortOnConnectFail = true,
            User = garnetOptions.UserName,
            Password = garnetOptions.Password
        };

        if (Debugger.IsAttached)
        {
            configOptions.SyncTimeout = (int)TimeSpan.FromHours(2).TotalMilliseconds;
            configOptions.AsyncTimeout = (int)TimeSpan.FromHours(2).TotalMilliseconds;
        }

        return configOptions;
    }
}
