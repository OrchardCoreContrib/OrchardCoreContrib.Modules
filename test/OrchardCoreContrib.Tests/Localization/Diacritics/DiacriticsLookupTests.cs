using System.Collections.Generic;
using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class DiacriticsLookupTests
    {
        [Fact]
        public void BuildDiacriticsLookup()
        {
            // Arrange
            var accentMappers = new List<IAccentMapper>
            {
                new ArabicAccentMapper(),
                new UkraniaAccentMapper()
            };

            // Act
            var lookup = new DiacriticsLookup(accentMappers);

            // Aseert
            Assert.Equal(2, lookup.Count);
            Assert.Equal(7, lookup["ar"].Mapping.Count);
            Assert.Equal(4, lookup["uk"].Mapping.Count);
        }

        [Fact]
        public void UseFirstMapperIfMultiplerAccentMappersProvidedForTheSameCulture()
        {
            // Arrange
            var alifAccentMapper = new ArabicAlifAccentMapper();
            var accentMappers = new List<IAccentMapper>
            {
                alifAccentMapper,
                new ArabicWawAccentMapper(),
                new ArabicYaAccentMapper()
            };

            // Act
            var lookup = new DiacriticsLookup(accentMappers);

            // Aseert
            Assert.True(lookup.Contains("ar"));
            Assert.Equal(alifAccentMapper.Mapping.Count, lookup["ar"].Mapping.Count);
        }
    }
}
