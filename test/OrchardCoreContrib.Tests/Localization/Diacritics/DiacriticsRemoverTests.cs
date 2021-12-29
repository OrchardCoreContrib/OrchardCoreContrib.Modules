using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class DiacriticsRemoverTests
    {
        [Theory]
        [InlineData("Jan Łukasiewicz", "Jan Lukasiewicz")]
        [InlineData("Wacław Sierpiński", "Waclaw Sierpinski")]
        public void RemoveDiacriticFromCustomerAccentMapper_IfLookupContainsTheCurrentCulture(string text, string expected)
        {
            // Arrange
            var accentMappers = new List<IAccentMapper>
            {
                new PolishAccentMapper()
            };
            var lookup = new DiacriticsLookup(accentMappers);
            var remover = new DiacriticsRemover(lookup);

            SetCurrentCulture("pl");

            // Act
            var result = remover.Remove(text);

            // Aseert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Jan Łukasiewicz", "Jan Łukasiewicz")]
        [InlineData("Wacław Sierpiński", "Wacław Sierpinski")]
        public void IgnoreRemovingDiacriticFromCustomerAccentMapper_IfLookupDoesNotContainsTheCurrentCulture(string text, string expected)
        {
            // Arrange
            var accentMappers = new List<IAccentMapper>
            {
                new PolishAccentMapper()
            };
            var lookup = new DiacriticsLookup(accentMappers);
            var remover = new DiacriticsRemover(lookup);

            SetCurrentCulture("ar");

            // Act
            var result = remover.Remove(text);

            // Aseert
            Assert.Equal(expected, result);
        }

        private void SetCurrentCulture(string culture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }
    }
}
