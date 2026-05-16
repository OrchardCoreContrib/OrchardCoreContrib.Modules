using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet.Tests;

public static class Utilities
{
    public static async Task<IGarnetService> CreateGarnetServiceAsync()
    {
        var garnetClientFactory = new GarnetClientFactory(
            Mock.Of<IHostApplicationLifetime>(),
            Mock.Of<ILogger<GarnetClientFactory>>());
        var garnetService = new GarnetService(
            garnetClientFactory,
            Options.Create(new GarnetOptions
            {
                Host = "127.0.0.1",
                Port = TestBase.Port,
            }));

        for (var attempt = 1; attempt <= 10 && garnetService.Client is null; attempt++)
        {
            await garnetService.ConnectAsync();

            if (garnetService.Client is null)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100 * attempt));
            }
        }

        if (garnetService.Client is null)
        {
            throw new InvalidOperationException("Unable to create a connected Garnet client for tests.");
        }

        return garnetService;
    }
}
