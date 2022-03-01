using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Data.Tests
{
    public class DataLocalizerTests
    {
        private readonly Mock<ILogger> _loggerMock;

        public DataLocalizerTests()
        {
            _loggerMock = new Mock<ILogger>();
        }

        [Fact]
        public void LocalizerReturnsTranslationsFromProvidedDictionary()
        {
            // Arrange
            var localizer = CreateDataLocalizer("fr", new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }));

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
            var localizer = CreateDataLocalizer("fr", new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }));

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
            var localizer = CreateDataLocalizer("fr", Array.Empty<CultureDictionaryRecord>());

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
            var localizer = CreateDataLocalizer(new Dictionary<string, IEnumerable<CultureDictionaryRecord>>
            {
                {"fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) } },
                { "fr-FR", new[] { new CultureDictionaryRecord("Bye", null, new[] { "au revoir" }) } }
            });

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
            var localizer = CreateDataLocalizer(new Dictionary<string, IEnumerable<CultureDictionaryRecord>>
            {
                { "fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) } },
                { "fr-FR", new[] { new CultureDictionaryRecord("Bye", null, new[] { "au revoir" }) }}
            });

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
            var localizer = CreateDataLocalizer("cs", new CultureDictionaryRecord("The page (ID:{0}) was deleted.", null, new[] { "Stránka (ID:{0}) byla smazána." }));

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
            var localizer = CreateDataLocalizer(new Dictionary<string, IEnumerable<CultureDictionaryRecord>>
            {
                { "ar", new CultureDictionaryRecord[] { new CultureDictionaryRecord("hello", null, new[] { "مرحبا" }) } },
                { "ar-YE", Array.Empty<CultureDictionaryRecord>()}
            }, fallBackToParentCulture);

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
            var localizer = CreateDataLocalizer(new Dictionary<string, IEnumerable<CultureDictionaryRecord>>
            {
                { "ar", new CultureDictionaryRecord[] {
                    new CultureDictionaryRecord("Blog", null, new[] { "مدونة" }),
                    new CultureDictionaryRecord("Menu", null, new[] { "قائمة" }),
                    new CultureDictionaryRecord("Page", null, new[] { "صفحة" }),
                    new CultureDictionaryRecord("Article", null, new[] { "مقالة" })
                } },
                { "ar-YE", new CultureDictionaryRecord[] {
                    new CultureDictionaryRecord("Blog", null, new[] { "مدونة" }),
                    new CultureDictionaryRecord("Product", null, new[] { "منتج" })
                } }
            });

            CultureInfo.CurrentUICulture = new CultureInfo("ar-YE");

            // Act
            var translations = localizer.GetAllStrings(includeParentCultures).Select(l => l.Value).ToArray();

            // Assert
            Assert.Equal(expected.Length, translations.Length);
        }

        private static IMemoryCache GetMemoryCache()
        {
            var serviceProvider = new ServiceCollection()
                .AddMemoryCache()
                .BuildServiceProvider();

            return serviceProvider.GetService<IMemoryCache>();
        }

        private IDataLocalizer CreateDataLocalizer(string cultureName, CultureDictionaryRecord cultureDictionaryRecord, bool fallBackToParent = true)
            => CreateDataLocalizer(cultureName, new[] { cultureDictionaryRecord }, fallBackToParent);

        private IDataLocalizer CreateDataLocalizer(IDictionary<string, IEnumerable<CultureDictionaryRecord>> cultureDictionaries, bool fallBackToParent = true)
        {
            var translationProvider = new InMemoryDataTranslationProvider();

            foreach (var dictionary in cultureDictionaries)
            {
                translationProvider.AddDictionary(dictionary.Key, dictionary.Value);

            }

            var localizer = new DataLocalizer(new DataResourceManager(translationProvider, GetMemoryCache()), fallBackToParent, _loggerMock.Object);

            return localizer;
        }

        private IDataLocalizer CreateDataLocalizer(string cultureName, IEnumerable<CultureDictionaryRecord> records, bool fallBackToParent = true)
        {
            var translationProvider = new InMemoryDataTranslationProvider();

            translationProvider.AddDictionary(cultureName, records);

            var localizer = new DataLocalizer(new DataResourceManager(translationProvider, GetMemoryCache()), fallBackToParent, _loggerMock.Object);

            return localizer;
        }
    }
}
