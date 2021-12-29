using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class AccentMapperTests
    {
        [Fact]
        public void CouldMapToMultipleCharacters()
        {
            // Arrange
            IAccentMapper mapper;

            // Act
            mapper = new FrenchAccentMapper();

            // Aseert
            Assert.True(mapper.Mapping.All(m => m.Value.Length > 1));
        }
    }
}
