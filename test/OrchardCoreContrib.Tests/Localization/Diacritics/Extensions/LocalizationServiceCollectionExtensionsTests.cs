using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Localization.Diacritics.Tests;
using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Extensions.Tests
{
    public class LocalizationServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDiacriticsShouldAddIDiacriticsLookup()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddDiacritics();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            
            Assert.NotNull(serviceProvider.GetService<IDiacriticsLookup>());
        }

        [Fact]
        public void AddCustomAccentMappersToDI()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddSingleton<IAccentMapper, ArabicAccentMapper>();
            services.AddDiacritics();
            services.AddSingleton<IAccentMapper, FrenchAccentMapper>();

            var serviceProvider = services.BuildServiceProvider();
            var lookup = serviceProvider.GetService<IDiacriticsLookup>();

            // Assert
            Assert.True(lookup.Contains("ar"));
            Assert.True(lookup.Contains("fr"));
        }
    }
}
