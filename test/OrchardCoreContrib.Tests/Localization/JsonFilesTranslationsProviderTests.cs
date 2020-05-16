using Microsoft.Extensions.FileProviders;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Json;
using Moq;
using Xunit;

namespace OrchardCoreContrib.Tests.Localization
{
    public class JsonFilesTranslationsProviderTests
    {
        private static readonly PluralizationRuleDelegate _frPluralizationRule = n => n > 1 ? 1 : 0;
        private static readonly string Namespace = typeof(JsonFilesTranslationsProviderTests).Namespace;
        private static readonly IFileProvider _fileProvider = new EmbeddedFileProvider(typeof(JsonFilesTranslationsProviderTests).Assembly, Namespace);

        private readonly Mock<ILocalizationFileLocationProvider> _localizationFileLocationProvider;

        public JsonFilesTranslationsProviderTests()
        {
            _localizationFileLocationProvider = new Mock<ILocalizationFileLocationProvider>();
            _localizationFileLocationProvider
                .Setup(l => l.GetLocations(It.IsAny<string>()))
                .Returns(() => _fileProvider.GetDirectoryContents(string.Empty));
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
            Assert.Equal("Bonjour", cultureDictionary.Translations["Hello"][0]);
            Assert.Equal("Oui", cultureDictionary.Translations["Yes"][0]);
            Assert.Equal("Non", cultureDictionary.Translations["No"][0]);
        }
    }
}
