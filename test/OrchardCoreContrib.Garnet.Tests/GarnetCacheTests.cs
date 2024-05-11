using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCoreContrib.Garnet.Services;
using System.Text;

namespace OrchardCoreContrib.Garnet.Tests;

public class GarnetCacheTests : TestBase
{
    private static IGarnetService _garnetService;

    public override async Task InitializeAsync()
    {
        _garnetService = await CreateGarnetServiceAsync();

        await _garnetService.Client.KeyDeleteAsync(["key1", "key2", "key3", "key4"]);
    }

    [Theory]
    [InlineData("key1", new byte[] { })]
    [InlineData("key2", new byte[] { 79, 67, 67 })]
    public async Task GetValue(string key, byte[] expectedValue)
    {
        // Arrange
        var cache = new GarnetCache(_garnetService);

        await _garnetService.Client.StringSetAsync("key2", "OCC");

        // Act
        var value = cache.Get(key);

        // Assert
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("key1", new byte[] { })]
    [InlineData("key2", new byte[] { 79, 67, 67 })]
    public async Task GetValueAsync(string key, byte[] expectedValue)
    {
        // Arrange
        var cache = new GarnetCache(_garnetService);

        await _garnetService.Client.StringSetAsync("key2", "OCC");

        // Act
        var value = await cache.GetAsync(key);

        // Assert
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public async Task SetValue()
    {
        // Arrange
        var key = "key3";
        var value = "OCC"u8.ToArray();
        var cache = new GarnetCache(_garnetService);

        // Act
        cache.Set(key, value, default);

        // Assert
        Assert.Equal(Encoding.UTF8.GetString(value), await _garnetService.Client.StringGetAsync("key3"));
    }

    [Fact]
    public async Task SetValueAsync()
    {
        // Arrange
        var key = "key4";
        var value = "OCC"u8.ToArray();
        var cache = new GarnetCache(_garnetService);

        // Act
        await cache.SetAsync(key, value, default);

        // Assert
        Assert.Equal(Encoding.UTF8.GetString(value), await _garnetService.Client.StringGetAsync("key4"));
    }

    [Fact]
    public async Task RemoveValue()
    {
        // Arrange
        var key = "key5";
        var cache = new GarnetCache(_garnetService);

        await _garnetService.Client.StringSetAsync(key, "OCC");

        // Act
        cache.Remove(key);

        // Assert
        Assert.Null(await _garnetService.Client.StringGetAsync(key));
    }

    [Fact]
    public async Task RemoveValueAsync()
    {
        // Arrange
        var key = "key6";
        var cache = new GarnetCache(_garnetService);

        await _garnetService.Client.StringSetAsync(key, "OCC");

        // Act
        await cache.RemoveAsync(key);

        // Assert
        Assert.Null(await _garnetService.Client.StringGetAsync(key));
    }

    [Fact]
    public void RefreshValue()
    {
        // Arrange
        var cache = new GarnetCache(_garnetService);

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => cache.Refresh("key7"));
    }

    [Fact]
    public async Task RefreshValueAsync()
    {
        // Arrange
        var cache = new GarnetCache(_garnetService);

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(async () => await cache.RefreshAsync("key8"));
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