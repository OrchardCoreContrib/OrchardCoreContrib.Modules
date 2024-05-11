using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Caching.Distributed;
using OrchardCore.Environment.Shell;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net;

namespace OrchardCoreContrib.Garnet.Services;

/// <summary>
/// Represents a message bus implementation based on Garnet service.
/// </summary>
/// <param name="garnetService">The <see cref="IGarnetService"/>.</param>
/// <param name="garnetOptions">The <see cref="IOptions{GarnetOptions}"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="logger">The <see cref="ILogger{GarnetBus}"/>.</param>
public class GarnetBus(
    IGarnetService garnetService,
    IOptions<GarnetOptions> garnetOptions,
    ShellSettings shellSettings,
    ILogger<GarnetBus> logger) : IMessageBus
{
    private readonly GarnetOptions _garnetOptions = garnetOptions.Value;
    private readonly string _hostName = Dns.GetHostName() + ':' + Environment.ProcessId;
    private readonly string _channelPrefix = garnetService.InstancePrefix + shellSettings.Name + ':';

    /// <inheritdoc/>
    public async Task SubscribeAsync(string channel, Action<string, string> handler)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();

            if (garnetService.Client == null)
            {
                logger.LogError("Unable to subscribe to the channel '{ChannelName}'.", _channelPrefix + channel);

                return;
            }
        }

        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(GetConfigurationOptions(_garnetOptions));

        try
        {
            var subscriber = connectionMultiplexer.GetSubscriber();

            await subscriber.SubscribeAsync(RedisChannel.Literal(_channelPrefix + channel), (redisChannel, redisValue) =>
            {
                var tokens = redisValue.ToString().Split('/').ToArray();

                if (tokens.Length != 2 || tokens[0].Length == 0)
                {
                    return;
                }

                handler(channel, tokens[1]);
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to subscribe to the channel '{ChannelName}'.", _channelPrefix + channel);
        }
    }

    /// <inheritdoc/>
    public async Task PublishAsync(string channel, string message)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();
            
            if (garnetService.Client == null)
            {
                logger.LogError("Unable to publish to the channel '{ChannelName}'.", _channelPrefix + channel);

                return;
            }
        }

        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(GetConfigurationOptions(_garnetOptions));

        try
        {
            await connectionMultiplexer.GetSubscriber()
                .PublishAsync(RedisChannel.Literal(_channelPrefix + channel), $"{_hostName}/{message}");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to publish to the channel '{ChannelName}'.", _channelPrefix + channel);
        }
    }

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
