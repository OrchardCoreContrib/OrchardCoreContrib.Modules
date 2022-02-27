using Microsoft.Extensions.Logging;
using Moq;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Tests.Localization
{
    public class DataLocalizerTests
    {
        private static readonly PluralizationRuleDelegate _noPluralRule = n => 0;

        private readonly Mock<ILocalizationManager> _localizationManager;
        private readonly Mock<ILogger> _logger;

        public DataLocalizerTests()
        {
            _localizationManager = new Mock<ILocalizationManager>();
            _logger = new Mock<ILogger>();
        }

        [Fact]
        public void LocalizerReturnsTranslationsFromProvidedDictionary()
        {
            // Arrange
            SetupDictionary("fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) });

            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("fr");

            // Act
            var translation = localizer["Hello"];

            // Assert
            Assert.Equal("Bonjour", translation);
        }

        [Fact]
        public void LocalizerReturnsOriginalTextIfTranslationsDoesntExistInProvidedDictionary()
        {
            // Arrange
            SetupDictionary("fr", new[] {
                new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" })
            });

            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("fr");

            // Act
            var translation = localizer["Bye"];

            // Assert
            Assert.Equal("Bye", translation);
        }

        [Fact]
        public void LocalizerReturnsOriginalTextIfDictionaryIsEmpty()
        {
            // Arrange
            SetupDictionary("fr", Array.Empty<CultureDictionaryRecord>());

            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("fr");

            // Act
            var translation = localizer["Hello"];

            // Assert
            Assert.Equal("Hello", translation);
        }

        [Fact]
        public void LocalizerFallbacksToParentCultureIfTranslationDoesntExistInSpecificCulture()
        {
            // Arrange
            SetupDictionary("fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) });
            SetupDictionary("fr-FR", new[] { new CultureDictionaryRecord("Bye", null, new[] { "au revoir" }) });

            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");

            // Act
            var translation = localizer["Hello"];

            // Assert
            Assert.Equal("Bonjour", translation);
        }

        [Fact]
        public void LocalizerReturnsTranslationFromSpecificCultureIfItExists()
        {
            // Arrange
            SetupDictionary("fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) });
            SetupDictionary("fr-FR", new[] { new CultureDictionaryRecord("Bye", null, new[] { "au revoir" }) });

            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");

            // Act
            var translation = localizer["Bye"];

            // Assert
            Assert.Equal("au revoir", translation);
        }

        [Fact]
        public void LocalizerReturnsFormattedTranslation()
        {
            // Arrange
            SetupDictionary("cs", new[] { new CultureDictionaryRecord("The page (ID:{0}) was deleted.", null, new[] { "Stránka (ID:{0}) byla smazána." }) });
            var localizer = new DataLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo("cs");

            // Act
            var translation = localizer["The page (ID:{0}) was deleted.", 1];

            // Assert
            Assert.Equal("Stránka (ID:1) byla smazána.", translation);
        }

        [Theory]
        [InlineData(false, "hello", "hello")]
        [InlineData(true, "hello", "مرحبا")]
        public void LocalizerFallBackToParentCultureIfFallBackToParentUICulturesIsTrue(bool fallBackToParentCulture, string resourceKey, string expected)
        {
            // Arrange
            SetupDictionary("ar", new CultureDictionaryRecord[] { new CultureDictionaryRecord("hello", null, new[] { "مرحبا" }) });
            SetupDictionary("ar-YE", Array.Empty<CultureDictionaryRecord>());
            
            var localizer = new DataLocalizer(_localizationManager.Object, fallBackToParentCulture, _logger.Object);
            
            CultureInfo.CurrentUICulture = new CultureInfo("ar-YE");
            
            // Act
            var translation = localizer[resourceKey];

            // Assert
            Assert.Equal(expected, translation);
        }

        [Theory]
        [InlineData(false, new[] { "مدونة", "منتج" })]
        [InlineData(true, new[] { "مدونة", "منتج", "قائمة", "صفحة", "مقالة" })]
        public void LocalizerReturnsGetAllStrings(bool includeParentCultures, string[] expected)
        {
            // Arrange
            SetupDictionary("ar", new CultureDictionaryRecord[] {
                new CultureDictionaryRecord("Blog", null, new[] { "مدونة" }),
                new CultureDictionaryRecord("Menu", null, new[] { "قائمة" }),
                new CultureDictionaryRecord("Page", null, new[] { "صفحة" }),
                new CultureDictionaryRecord("Article", null, new[] { "مقالة" })
            });
            SetupDictionary("ar-YE", new CultureDictionaryRecord[] {
                new CultureDictionaryRecord("Blog", null, new[] { "مدونة" }),
                new CultureDictionaryRecord("Product", null, new[] { "منتج" })
            });

            var localizer = new DataLocalizer(_localizationManager.Object, false, _logger.Object);
            
            CultureInfo.CurrentUICulture = new CultureInfo("ar-YE");
            
            // Act
            var translations = localizer.GetAllStrings(includeParentCultures).Select(l => l.Value).ToArray();

            // Assert
            Assert.Equal(expected.Length, translations.Length);
        }

        private void SetupDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records)
        {
            var dictionary = new CultureDictionary(cultureName, _noPluralRule);
            
            dictionary.MergeTranslations(records);

            _localizationManager
                .Setup(o => o.GetDictionary(It.Is<CultureInfo>(c => c.Name == cultureName)))
                .Returns(dictionary);
        }
    }
}
