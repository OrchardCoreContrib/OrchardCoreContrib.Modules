using OrchardCoreContrib.Garnet.Services;
using System.Xml.Linq;

namespace OrchardCoreContrib.Garnet.Tests;

public class GarnetXmlRepositoryTests : TestBase
{
    private static IGarnetService _garnetService;

    public override async Task InitializeAsync()
    {
        _garnetService = await Utilities.CreateGarnetServiceAsync();

        await _garnetService.Client.KeyDeleteAsync(["xmlKey1", "xmlKey2", "xmlKey4"]);

        await _garnetService.Client.ExecuteForStringResultAsync("RPUSH", ["xmlKey1", "<element><child1>value1</child1></element>"]);

        for (int i = 1; i <= 3; i++)
        {
            await _garnetService.Client.ExecuteForStringResultAsync("RPUSH", ["xmlKey2", "<element><child1>value1</child1></element>"]);
        }
    }

    [Theory]
    [InlineData("xmlKey1", 1)]
    [InlineData("xmlKey2", 3)]
    [InlineData("xmlKey3", 0)]
    public void GetAllElements(string key, int expectedElementsCount)
    {
        // Arrange
        var garnetXmlRepository = new GarnetXmlRepository(() => _garnetService.Client, key);

        // Act
        var elements = garnetXmlRepository.GetAllElements();
        
        // Assert
        Assert.Equal(expectedElementsCount, elements.Count);
    }

    [Fact]
    public void StoreElement()
    {
        // Arrange
        var element = XElement.Parse("<element><child1>value1</child1></element>");
        var garnetXmlRepository = new GarnetXmlRepository(() => _garnetService.Client, "xmlKey4");

        // Act
        garnetXmlRepository.StoreElement(element, null);

        // Assert
        var elements = garnetXmlRepository.GetAllElements();
        Assert.Single(elements);
        Assert.Equal(element.ToString(), elements.Single().ToString());
    }
}
