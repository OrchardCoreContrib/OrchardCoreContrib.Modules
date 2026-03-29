using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Caching.Distributed;
using OrchardCore.Environment.Shell;
using StackExchange.Redis;
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
    private readonly ConfigurationOptions _configurationOptions = GarnetOptionsConverter.ConvertToConfigurationOptions(garnetOptions.Value);
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

        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(_configurationOptions);

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

        var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(_configurationOptions);

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
}
