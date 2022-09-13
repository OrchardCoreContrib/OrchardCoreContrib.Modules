using Microsoft.Extensions.FileProviders;
using Moq;
using OrchardCore.Localization;
using System.Collections.Generic;
using Xunit;

namespace OrchardCoreContrib.Localization.Json.Tests;

public class JsonFilesTranslationsProviderTests
{
    private static readonly PluralizationRuleDelegate _frPluralizationRule = n => n > 1 ? 1 : 0;

    private readonly Mock<ILocalizationFileLocationProvider> _localizationFileLocationProvider;

    public JsonFilesTranslationsProviderTests()
    {
        var embeddedFileProvider = new EmbeddedFileProvider(typeof(JsonFilesTranslationsProviderTests).Assembly);

        _localizationFileLocationProvider = new Mock<ILocalizationFileLocationProvider>();
        _localizationFileLocationProvider
        .Setup(l => l.GetLocations(It.IsAny<string>()))
        .Returns(() => new List<IFileInfo>
        {
                embeddedFileProvider.GetFileInfo("Files.First.json"),
                embeddedFileProvider.GetFileInfo("Files.Second.json")
        });
    }

    [Fact]
    public void JsonFilesTranslationsProvider_LoadTranslations()
    {
        // Arrange
        var culture = "fr-FR";
        var translationProvider = new JsonFilesTranslationsProvider(_localizationFileLocationProvider.Object);
        var cultureDictionary = new CultureDictionary(culture, _frPluralizationRule);

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
