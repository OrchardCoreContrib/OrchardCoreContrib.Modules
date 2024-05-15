using StackExchange.Redis;
using System.Diagnostics;
using System.Net;

namespace OrchardCoreContrib.Garnet;

internal class GarnetOptionsConverter
{
    public static ConfigurationOptions ConvertToConfigurationOptions(GarnetOptions garnetOptions)
    {
        var endPoints = new EndPointCollection
        {
            new DnsEndPoint(garnetOptions.Host, garnetOptions.Port)
        };
        var configurationOptions = new ConfigurationOptions
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
            configurationOptions.SyncTimeout = (int)TimeSpan.FromHours(2).TotalMilliseconds;
            configurationOptions.AsyncTimeout = (int)TimeSpan.FromHours(2).TotalMilliseconds;
        }

        return configurationOptions;
    }
}
