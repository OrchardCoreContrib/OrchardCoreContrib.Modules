using Moq;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Xliff.Tests;

public class XliffStringLocalizerTests
{
    private static readonly PluralizationRuleDelegate _defaultPluralizationRule = n => n != 1 ? 1 : 0;
    private readonly Mock<ILocalizationManager> _localizationManager;

    public XliffStringLocalizerTests()
    {
        _localizationManager = new Mock<ILocalizationManager>();
    }

    [Fact]
    public void XliffStringLocalizer_ReturnsTranslationFromProvidedDictionary()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary(culture, new[] { new CultureDictionaryRecordWrapper("Hello", "Bonjour") });

        var localizer = new XliffStringLocalizer(_localizationManager.Object, true);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Bonjour", translation);
    }

    [Fact]
    public void XliffStringLocalizer_ReturnsOriginalText_IfTranslationDoesntExistInProvidedDictionary()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary(culture, new[] { new CultureDictionaryRecordWrapper("Hello", "Bonjour") });

        var localizer = new XliffStringLocalizer(_localizationManager.Object, false);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Bye"];

        // Assert
        Assert.Equal("Bye", translation);
    }

    [Fact]
    public void XliffStringLocalizer_ReturnsOriginalText_IfDictionaryIsEmpty()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary(culture, Array.Empty<CultureDictionaryRecord>());

        var localizer = new XliffStringLocalizer(_localizationManager.Object, false);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Hello", translation);
    }

    [Fact]
    public void XliffStringLocalizer_FallbacksToParentCulture_IfTranslationDoesNotExistInSpecificCulture()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary("fr", new[] { new CultureDictionaryRecordWrapper("Hello", "Bonjour") });
        SetupDictionary(culture, new[] { new CultureDictionaryRecordWrapper("Bye", "au revoir") });

        var localizer = new XliffStringLocalizer(_localizationManager.Object, true);

        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer["Hello"];

        // Assert
        Assert.Equal("Bonjour", translation);
    }

    [Fact]
    public void XliffStringLocalizer_ReturnsTranslationFromSpecificCulture_IfItExists()
    {
        // Arrange
        var culture = "fr-FR";
        SetupDictionary("fr", new[] { new CultureDictionaryRecordWrapper("Hello", "Bonjour (fr)") });
        SetupDictionary(culture, new[] { new CultureDictionaryRecordWrapper("Hello", "Bonjour (fr-FR)") });
        var localizer = new XliffStringLocalizer(_localizationManager.Object, false);

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
        SetupDictionary("ar", new CultureDictionaryRecord[] { new CultureDictionaryRecordWrapper("Hello", "مرحبا") });
        SetupDictionary(culture, Array.Empty<CultureDictionaryRecord>());
        var localizer = new XliffStringLocalizer(_localizationManager.Object, fallBackToParentCulture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translation = localizer[resourceKey];

        // Assert
        Assert.Equal(expected, translation);
    }

    [Theory]
    [InlineData(false, new[] { "مدونة", "منتج" })]
    [InlineData(true, new[] { "مدونة", "منتج", "قائمة", "صفحة", "مقالة" })]
    public void XliffStringLocalizer_GetAllStrings(bool includeParentCultures, string[] expected)
    {
        // Arrange
        var culture = "ar-YE";
        SetupDictionary("ar", new CultureDictionaryRecord[] {
                new CultureDictionaryRecordWrapper("Blog", "مدونة" ),
                new CultureDictionaryRecordWrapper("Menu", "قائمة"),
                new CultureDictionaryRecordWrapper("Page", "صفحة"),
                new CultureDictionaryRecordWrapper("Article", "مقالة")
            });
        SetupDictionary(culture, new CultureDictionaryRecord[] {
                new CultureDictionaryRecordWrapper("Blog", "مدونة" ),
                new CultureDictionaryRecordWrapper("Product", "منتج" )
            });

        var localizer = new XliffStringLocalizer(_localizationManager.Object, false);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // Act
        var translations = localizer.GetAllStrings(includeParentCultures).Select(l => l.Value).ToArray();

        // Assert
        Assert.Equal(expected.Length, translations.Length);
    }

    private void SetupDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records)
    {
        var dictionary = new CultureDictionary(cultureName, _defaultPluralizationRule);
        dictionary.MergeTranslations(records);

        _localizationManager.Setup(o => o.GetDictionary(It.Is<CultureInfo>(c => c.Name == cultureName))).Returns(dictionary);
    }
}
