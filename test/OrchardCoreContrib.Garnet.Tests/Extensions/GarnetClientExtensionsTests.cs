using Garnet.client;
using OrchardCoreContrib.Garnet.Tests;

namespace OrchardCoreContrib.Garnet.Extensions.Tests;

public class GarnetClientExtensionsTests : TestBase
{
    private static GarnetClient _garnetClient;

    public override async Task InitializeAsync()
    {
        _garnetClient = (await Utilities.CreateGarnetServiceAsync()).Client;

        await _garnetClient.KeyDeleteAsync(["set_key1"]);
        await _garnetClient.KeyDeleteAsync(["list_key1"]);
        await _garnetClient.KeyDeleteAsync(["set_key2"]);
        await _garnetClient.KeyDeleteAsync(["list_key2"]);
        await _garnetClient.ExecuteForStringResultAsync("SADD", ["set_key1", "foo", "bar", "baz"]);
        await _garnetClient.ExecuteForStringResultAsync("RPUSH", ["list_key1", "foo", "bar", "baz"]);
    }

    [Fact]
    public async Task GetSetElements()
    {
        // Arrange
        var key = "set_key1";

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
        var key = "set_key2";

        // Act
        await _garnetClient.SetSetAsync(key, "foo");
        await _garnetClient.SetSetAsync(key, "bar");

        // Assert
        var values = await _garnetClient.ExecuteForStringArrayResultAsync("SMEMBERS", [key]);
        Assert.NotEmpty(values);
        Assert.Equal(["foo", "bar"], values);
    }

    [Theory]
    [InlineData(0, -1, new string[] { "foo", "bar", "baz" })]
    [InlineData(0, 0, new string[] { "foo" })]
    [InlineData(1, 2, new string[] { "bar", "baz" })]
    [InlineData(-3, 1, new string[] { "foo", "bar" })]
    [InlineData(-3, 2, new string[] { "foo", "bar", "baz" })]
    [InlineData(-100, 100, new string[] { "foo", "bar", "baz" })]
    public async Task GetListElements(int start, int stop, string[] expectedValues)
    {
        // Arrange
        var key = "list_key1";

        // Act
        var values = await _garnetClient.ListRangeAsync(key, start, stop);

        // Assert
        Assert.NotEmpty(values);
        Assert.Equal(expectedValues, values);
    }

    [Fact]
    public async Task PushListElement()
    {
        // Arrange
        var key = "list_key2";
        string[] items = ["foo", "bar"];

        // Act
        await _garnetClient.ListRightPushAsync(key, items[0]);
        await _garnetClient.ListRightPushAsync(key, items[1]);

        // Assert
        var values = await _garnetClient.ExecuteForStringArrayResultAsync("LRANGE", [key, "0", "-1"]);
        Assert.NotEmpty(values);
        Assert.Equal(items, values);
    }
}
