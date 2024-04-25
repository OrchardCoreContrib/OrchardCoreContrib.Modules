using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Localization.Data.Tests;

public class DataResourceStringProviderTests
{
    [Fact]
    public async Task GetResourceStrings()
    {
        // Arrange
        var resourceStringProvider = new TestResourceStringProvider();

        // Act
        var resourceStrings = await resourceStringProvider.GetAllResourceStringsAsync();

        // Assert
        Assert.Equal(4, resourceStrings.Count());
        Assert.Equal("content type|Article", resourceStrings.ElementAt(0));
        Assert.Equal("menu|Article", resourceStrings.ElementAt(1));
        Assert.Equal("content field|First Name", resourceStrings.ElementAt(2));
        Assert.Equal("content field|Last Name", resourceStrings.ElementAt(3));
    }
}
