using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization;
using OrchardCoreContrib.Localization.Json;
using Moq;
using Xunit;

namespace OrchardCoreContrib.Tests.Localization
{
    public class JsonStringLocalizerTests
    {
        private static readonly PluralizationRuleDelegate _frPluralizationRule = n => n > 1 ? 1 : 0;
        private readonly Mock<ILocalizationManager> _localizationManager;
        private readonly Mock<ILogger> _logger;

        public JsonStringLocalizerTests()
        {
            _localizationManager = new Mock<ILocalizationManager>();
            _logger = new Mock<ILogger>();
        }

        [Fact]
        public void JsonStringLocalizer_ReturnsTranslationFromProvidedDictionary()
        {
            // Arrange
            var culture = "fr-FR";
            SetupDictionary(culture, new[] {
                new CultureDictionaryRecordWrapper("Hello", "Bonjour")
            }, _frPluralizationRule);

            var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            // Act
            var translation = localizer["Hello"];

            // Assert
            Assert.Equal("Bonjour", translation);
        }

        [Fact]
        public void JsonStringLocalizer_ReturnsOriginalText_IfTranslationDoesntExistInProvidedDictionary()
        {
            // Arrange
            var culture = "fr-FR";
            SetupDictionary(culture, new[] {
                new CultureDictionaryRecordWrapper("Hello", "Bonjour")
            }, _frPluralizationRule);

            var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            // Act
            var translation = localizer["Bye"];

            // Assert
            Assert.Equal("Bye", translation);
        }

        [Fact]
        public void JsonStringLocalizer_ReturnsOriginalText_IfDictionaryIsEmpty()
        {
            // Arrange
            var culture = "fr-FR";
            SetupDictionary(culture, new CultureDictionaryRecord[] { }, _frPluralizationRule);

            var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            var translation = localizer["Hello"];

            Assert.Equal("Hello", translation);
        }

        private void SetupDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records, PluralizationRuleDelegate pluralRule)
        {
            var dictionary = new CultureDictionary(cultureName, pluralRule);
            dictionary.MergeTranslations(records);

            _localizationManager.Setup(o => o.GetDictionary(It.Is<CultureInfo>(c => c.Name == cultureName))).Returns(dictionary);
        }
    }
}
