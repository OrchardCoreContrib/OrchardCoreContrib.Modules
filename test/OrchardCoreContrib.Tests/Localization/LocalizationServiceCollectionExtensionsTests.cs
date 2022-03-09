using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Localization.Data;
using Xunit;

namespace OrchardCoreContrib.Localization.Tests
{
    public class LocalizationServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDataLocaliztion_RegisterRequiredServices()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddMemoryCache();

            // Act
            services.AddDataLocalization();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var dataLocalizerFactory = serviceProvider.GetService<IDataLocalizerFactory>();
            var dataLocalizer = serviceProvider.GetService<IDataLocalizer>();

            Assert.NotNull(dataLocalizerFactory);
            Assert.IsType<DataLocalizerFactory>(dataLocalizerFactory);
            Assert.NotNull(dataLocalizer);
            Assert.IsType<DataLocalizer>(dataLocalizer);
        }
    }
}
