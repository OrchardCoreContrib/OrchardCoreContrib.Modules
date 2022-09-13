using Microsoft.Extensions.Logging;
using Moq;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Json.Tests;

public class JsonStringLocalizerTests
{
    private static readonly PluralizationRuleDelegate _arPluralizationRule = n => (n == 0 ? 0 : n == 1 ? 1 : n == 2 ? 2 : n % 100 >= 3 && n % 100 <= 10 ? 3 : n % 100 >= 11 ? 4 : 5);
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
        SetupDictionary(culture, Array.Empty<CultureDictionaryRecord>(), _frPluralizationRule);

        var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Hello", translation);
    }

    [Fact]
    public void JsonStringLocalizer_FallbacksToParentCulture_IfTranslationDoesntExistInSpecificCulture()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary("fr", new[] {
                new CultureDictionaryRecordWrapper("Hello", "Bonjour")
            }, _frPluralizationRule);
        SetupDictionary(culture, new[] {
                new CultureDictionaryRecordWrapper("Bye", "au revoir")
            }, _frPluralizationRule);

        var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Bonjour", translation);
    }

    [Fact]
    public void JsonStringLocalizer_ReturnsTranslationFromSpecificCulture_IfItExists()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary("fr", new[] {
                new CultureDictionaryRecordWrapper("Hello", "Bonjour (fr)")
            }, _frPluralizationRule);
        SetupDictionary(culture, new[] {
                new CultureDictionaryRecordWrapper("Hello", "Bonjour (fr-FR)")
            }, _frPluralizationRule);
        var localizer = new JsonStringLocalizer(_localizationManager.Object, true, _logger.Object);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Bonjour (fr-FR)", translation);
    }

    [Theory]
    [InlineData(false, "Hello", "Hello")]
    [InlineData(true, "Hello", "مرحبا")]
    public void LocalizerFallBackToParentCultureIfFallBackToParentUICulturesIsTrue(bool fallBackToParentCulture, string resourceKey, string expected)
    {
        // Arrange
        var culture = "ar-YE";
        SetupDictionary("ar", new CultureDictionaryRecord[] {
                new CultureDictionaryRecordWrapper("Hello", "مرحبا" )
            }, _arPluralizationRule);
        SetupDictionary(culture, Array.Empty<CultureDictionaryRecord>(), _arPluralizationRule);
        var localizer = new JsonStringLocalizer(_localizationManager.Object, fallBackToParentCulture, _logger.Object);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer[resourceKey];

        // Assert
        Assert.Equal(expected, translation);
    }

    [Theory]
    [InlineData(false, new[] { "مدونة", "منتج" })]
    [InlineData(true, new[] { "مدونة", "منتج", "قائمة", "صفحة", "مقالة" })]
    public void JsonStringLocalizer_GetAllStrings(bool includeParentCultures, string[] expected)
    {
        // Arrange
        var culture = "ar-YE";
        SetupDictionary("ar", new CultureDictionaryRecord[] {
                new CultureDictionaryRecordWrapper("Blog", "مدونة" ),
                new CultureDictionaryRecordWrapper("Menu", "قائمة"),
                new CultureDictionaryRecordWrapper("Page", "صفحة"),
                new CultureDictionaryRecordWrapper("Article", "مقالة")
            }, _arPluralizationRule);
        SetupDictionary(culture, new CultureDictionaryRecord[] {
                new CultureDictionaryRecordWrapper("Blog", "مدونة" ),
                new CultureDictionaryRecordWrapper("Product", "منتج" )
            }, _arPluralizationRule);

        var localizer = new JsonStringLocalizer(_localizationManager.Object, false, _logger.Object);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translations = localizer.GetAllStrings(includeParentCultures).Select(l => l.Value).ToArray();

        // Assert
        Assert.Equal(expected.Count(), translations.Count());
    }

    private void SetupDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records, PluralizationRuleDelegate pluralRule)
    {
        var dictionary = new CultureDictionary(cultureName, pluralRule);
        dictionary.MergeTranslations(records);

        _localizationManager.Setup(o => o.GetDictionary(It.Is<CultureInfo>(c => c.Name == cultureName))).Returns(dictionary);
    }
}
