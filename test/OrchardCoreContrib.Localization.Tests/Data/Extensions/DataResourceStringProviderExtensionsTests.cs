using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Localization.Data.Tests;

public class DataResourceStringProviderExtensionsTests
{
    [Theory]
    [InlineData("Content Type", 1)]
    [InlineData("Menu", 1)]
    [InlineData("Content Field", 2)]
    [InlineData("content field", 2)]
    [InlineData("None", 0)]
    public async Task GetResourceStrings(string context, int expectedResourcesNumber)
    {
        // Arrange
        var resourceStringProvider = new TestResourceStringProvider();

        // Act
        var resourceStrings = await resourceStringProvider.GetAllResourceStringsAsync(context);

        // Assert
        Assert.Equal(expectedResourcesNumber, resourceStrings.Count());
    }
}
