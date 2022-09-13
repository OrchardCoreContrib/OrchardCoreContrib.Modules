using Microsoft.Extensions.FileProviders;
using Moq;
using OrchardCore.Localization;
using OrchardCoreContrib.Tests.Common;
using System.Collections.Generic;
using Xunit;

namespace OrchardCoreContrib.Localization.Xliff.Tests;

public class XliffFilesTranslationsProviderTests
{
    private static readonly PluralizationRuleDelegate _defaultPluralizationRule = n => n != 1 ? 1 : 0;

    private readonly Mock<ILocalizationFileLocationProvider> _localizationFileLocationProvider;

    public XliffFilesTranslationsProviderTests()
    {
        var provider = new EmbeddedResourceReader().FileProvider;
        _localizationFileLocationProvider = new Mock<ILocalizationFileLocationProvider>();
        _localizationFileLocationProvider
            .Setup(l => l.GetLocations(It.IsAny<string>()))
            .Returns(() => new List<IFileInfo>
            {
                provider.GetFileInfo("Localization.Xliff.Files.First.xliff"),
                provider.GetFileInfo("Localization.Xliff.Files.Second.xliff")
            });
    }

    [Fact]
    public void XliffFilesTranslationsProvider_LoadTranslations()
    {
        // Arrange
        var culture = "fr-FR";
        var translationProvider = new XliffFilesTranslationsProvider(_localizationFileLocationProvider.Object);
        var cultureDictionary = new CultureDictionary(culture, _defaultPluralizationRule);

        // Act
        translationProvider.LoadTranslations(culture, cultureDictionary);

        // Assert
        Assert.NotNull(cultureDictionary.Translations);
        Assert.Equal(3, cultureDictionary.Translations.Count);
        Assert.Equal("Bonjour", cultureDictionary.Translations[new CultureDictionaryRecordKey("Hello")][0]);
        Assert.Equal("Oui", cultureDictionary.Translations[new CultureDictionaryRecordKey("Yes")][0]);
        Assert.Equal("Non", cultureDictionary.Translations[new CultureDictionaryRecordKey("No")][0]);
    }
}
