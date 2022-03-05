using OrchardCore.Localization;
using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Data.Tests
{
    public class DataResourceStringProviderExtensionsTests
    {
        [Theory]
        [InlineData("Content Type", 1)]
        [InlineData("Menu", 1)]
        [InlineData("Content Field", 2)]
        [InlineData("content field", 2)]
        [InlineData("None", 0)]
        public void GetResourceStrings(string context, int expectedResourcesNumber)
        {
            // Arrange
            var resourceStringProvider = new TestResourceStringProvider();

            // Act
            var resourceStrings = resourceStringProvider.GetAllResourceStrings(context);

            // Assert
            Assert.Equal(expectedResourcesNumber, resourceStrings.Count());
        }
    }
}
