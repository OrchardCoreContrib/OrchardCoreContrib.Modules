using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Garnet.Tests;

namespace OrchardCoreContrib.Garnet.Services.Tests;

public class GarnetBusTests : TestBase
{
    private static GarnetBus _garnetBus;
    private static IGarnetService _garnetService;

    public override async Task InitializeAsync()
    {
        _garnetService = await CreateGarnetServiceAsync();

        _garnetBus = new GarnetBus(
            _garnetService,
            Options.Create(new GarnetOptions()),
            new ShellSettings(),
            NullLogger<GarnetBus>.Instance);

        await Task.CompletedTask;
    }

    [Fact]
    public async Task SubscribeToMessage()
    {
        // Arrange
        var @event = new ManualResetEvent(false);
        var channel = "chat:general";
        var message = "Hello World!!";
        var recieved = false;

        // Act & Assert
        await _garnetBus.SubscribeAsync(channel, (c, m) =>
        {
            Assert.Equal(channel, c);
            Assert.Equal(message, m);

            recieved = true;
        });

        int repeat = 5;
        while (!recieved)
        {
            await _garnetBus.PublishAsync(channel, message);

            if (@event.WaitOne(TimeSpan.FromSeconds(1)))
            {
                break;
            }
            
            repeat--;
            
            Assert.True(repeat != 0, "Timeout waiting for subsciption receive.");
        }
    }

    [Fact]
    public async Task PublishMessage()
    {
        // Arrange
        var command = "subscribe";
        var channel = "chat:general";

        // Act
        await _garnetBus.PublishAsync(channel, "Hello World!!");

        // Assert
        for (int i = 1; i <= 5; i++)
        {
            var results = await _garnetService.Client.ExecuteForStringArrayResultAsync(command, [channel]);
            Assert.Equal(3, results.Length);
            Assert.Equal([command, channel, i.ToString()], results);
        }
    }

    private static async Task<IGarnetService> CreateGarnetServiceAsync()
    {
        var garnetClientFactory = new GarnetClientFactory(
            Mock.Of<IHostApplicationLifetime>(),
            Mock.Of<ILogger<GarnetClientFactory>>());
        var garnetService = new GarnetService(garnetClientFactory, Options.Create(new GarnetOptions()));

        await garnetService.ConnectAsync();

        return garnetService;
    }
}
