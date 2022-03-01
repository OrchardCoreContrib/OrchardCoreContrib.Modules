using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Localization;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace OrchardCoreContrib.Localization.Data.Tests
{
    public class DataLocalizerFactoryTests
    {
        private static readonly PluralizationRuleDelegate _noPluralRule = n => 0;

        private readonly Mock<DataResourceManager> _dataResourceManagerMock;

        public DataLocalizerFactoryTests()
        {
            _dataResourceManagerMock = new Mock<DataResourceManager>(Mock.Of<IDataTranslationProvider>(), Mock.Of<IMemoryCache>());
        }

        [Fact]
        public void CreateDataLocalizer()
        {
            // Arrange
            SetupDictionary("fr", new[] { new CultureDictionaryRecord("Hello", null, new[] { "Bonjour" }) });

            var requestlocalizationOptions = Options.Create(new RequestLocalizationOptions { FallBackToParentUICultures = true });
            var loggerMock = new Mock<ILogger<DataLocalizerFactory>>();
            var localizerFactory = new DataLocalizerFactory(_dataResourceManagerMock.Object, requestlocalizationOptions, loggerMock.Object);

            // Act
            var localizer = localizerFactory.Create();

            CultureInfo.CurrentUICulture = new CultureInfo("fr");

            // Assert
            Assert.NotNull(localizer);
            Assert.Single(localizer.GetAllStrings());
        }

        private void SetupDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records)
        {
            var dictionary = new CultureDictionary(cultureName, _noPluralRule);
            dictionary.MergeTranslations(records);

            _dataResourceManagerMock
                .Setup(rm => rm.GetResources(It.Is<CultureInfo>(c => c.Name == cultureName), It.IsAny<bool>()))
                .Returns(dictionary.Translations);
        }
    }
}
