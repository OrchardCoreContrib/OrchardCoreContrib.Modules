using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet.Tests;

public class GarnetTagCacheTests : TestBase
{
    private static GarnetTagCache _garnetTagCache;

    private IGarnetService _garnetService;
    private ShellSettings _shellSettings = new();

    public override async Task InitializeAsync()
    {
        _garnetService = await CreateGarnetServiceAsync();
        
        _garnetTagCache = new GarnetTagCache(
            _garnetService,
            _shellSettings,
            [],
            NullLogger<GarnetTagCache>.Instance);
    }

    [Fact]
    public async Task TagItems()
    {
        // Arrange
        var key = "tagKey1";
        string[] tags = ["tag1", "tag2", "tag3"];

        // Act
        await _garnetTagCache.TagAsync(key, tags);

        // Assert
        foreach (var tag in tags)
        {
            var result = await _garnetService.Client.SetGetAsync($"{_garnetService.InstancePrefix}{_shellSettings.Name}:Tag:{tag}");
            Assert.Single(result);
            Assert.Equal(key, result[0]);
        }
    }

    [Fact]
    public async Task GetTaggedItems()
    {
        // Arrange
        var tag = "tag4";
        string[] items = ["item1", "item2"];

        await _garnetService.Client.SetSetAsync($"{_garnetService.InstancePrefix}{_shellSettings.Name}:Tag:{tag}", items[0]);
        await _garnetService.Client.SetSetAsync($"{_garnetService.InstancePrefix}{_shellSettings.Name}:Tag:{tag}", items[1]);

        // Act
        var taggedItems = await _garnetTagCache.GetTaggedItemsAsync(tag);

        // Assert
        Assert.NotEmpty(taggedItems);
        Assert.Equal(items, taggedItems);
    }

    [Fact]
    public async Task RemoveTaggedItem()
    {
        // Arrange
        var tag = "tag5";

        await _garnetService.Client.SetSetAsync($"{_garnetService.InstancePrefix}{_shellSettings.Name}:Tag:{tag}", "item1");

        // Act
        await _garnetTagCache.RemoveTagAsync(tag);

        // Assert
        await _garnetService.Client.SetGetAsync(tag);
        
        var result = await _garnetService.Client.SetGetAsync($"{_garnetService.InstancePrefix}{_shellSettings.Name}:Tag:{tag}");
        Assert.Empty(result);
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
