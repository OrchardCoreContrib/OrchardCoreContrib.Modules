using Garnet.client;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Garnet.Tests;
using System.Net;

namespace OrchardCoreContrib.Garnet.Services.Tests;

public class GarnetBusTests : TestBase
{
    private static GarnetBus _garnetBus;
    private static IGarnetService _garnetService;

    public override async Task InitializeAsync()
    {
        _garnetService = await Utilities.CreateGarnetServiceAsync();

        _garnetBus = new GarnetBus(
            _garnetService,
            Options.Create(new GarnetOptions
            {
                Host = "127.0.0.1",
                Port = TestBase.Port,
            }),
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
            @event.Set();
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

        // Assert - use a dedicated client to avoid putting the shared client into subscribe mode
        var endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), TestBase.Port);
        using var dedicatedClient = new GarnetClient(endpoint);
        await dedicatedClient.ConnectAsync();

        for (int i = 1; i <= 5; i++)
        {
            var results = await dedicatedClient.ExecuteForStringArrayResultAsync(command, [channel]);
            Assert.Equal(3, results.Length);
            Assert.Equal([command, channel, "1"], results);
        }
    }
}
