using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrchardCoreContrib.Data.Migrations;
using YesSql;

namespace OrchardCoreContrib.Data.Extensions.Tests;

public class DataMigrationsServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDataMigrationsShouldRegisterRequiredServices()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddTransient(sp => Mock.Of<ISession>());
        services.AddLogging();

        // Act
        services.AddDataMigrations();

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IMigrationLoader>());
        Assert.NotNull(serviceProvider.GetService<IMigrationRunner>());
    }
}
