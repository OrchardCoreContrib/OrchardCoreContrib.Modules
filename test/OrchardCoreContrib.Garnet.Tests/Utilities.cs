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
        var garnetService = new GarnetService(garnetClientFactory, Options.Create(new GarnetOptions()));

        await garnetService.ConnectAsync();

        return garnetService;
    }
}
