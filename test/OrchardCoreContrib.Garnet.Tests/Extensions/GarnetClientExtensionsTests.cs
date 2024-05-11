using Garnet.client;
using OrchardCoreContrib.Garnet.Tests;

namespace OrchardCoreContrib.Garnet.Extensions.Tests;

public class GarnetClientExtensionsTests : TestBase
{
    private static GarnetClient _garnetClient;

    public override async Task InitializeAsync()
    {
        _garnetClient = await CreateClientAsync();
        
        await _garnetClient.ExecuteForStringResultAsync("SADD", ["sets_key1", "foo", "bar", "baz"]);
        await _garnetClient.KeyDeleteAsync(["sets_key2"]);
    }

    [Fact]
    public async Task GetSetElements()
    {
        // Arrange
        var key = "sets_key1";

        // Act
        var values = await _garnetClient.SetGetAsync(key);

        // Assert
        Assert.NotEmpty(values);
        Assert.Equal(["foo", "bar", "baz"], values);
    }

    [Fact]
    public async Task AddSetElements()
    {
        // Arrange
        var key = "sets_key2";

        // Act
        await _garnetClient.SetSetAsync(key, "foo");
        await _garnetClient.SetSetAsync(key, "bar");

        // Assert
        var values = await _garnetClient.ExecuteForStringArrayResultAsync("SMEMBERS", [key]);
        Assert.NotEmpty(values);
        Assert.Equal(["foo", "bar"], values);
    }

    private static async Task<GarnetClient> CreateClientAsync()
    {
        var garnetOptions = new GarnetOptions();

        var garnetClient = new GarnetClient(garnetOptions.Host, garnetOptions.Port);

        await garnetClient.ConnectAsync();

        return garnetClient;
    }
}
