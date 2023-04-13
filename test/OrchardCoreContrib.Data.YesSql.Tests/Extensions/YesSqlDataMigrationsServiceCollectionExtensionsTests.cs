using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrchardCoreContrib.Data.Migrations;
using OrchardCoreContrib.Data.YesSql.Migrations;
using YesSql;

namespace OrchardCoreContrib.YesSqlData.Extensions.Tests;

public class YesSqlDataMigrationsServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDataMigrationsShouldRegisterRequiredServices()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddTransient(sp => Mock.Of<ISession>());
        services.AddTransient(sp => Mock.Of<IStore>());
        services.AddLogging();

        // Act
        services.AddYesSqlDataMigrations();

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IMigrationLoader>());
        Assert.NotNull(serviceProvider.GetService<IMigrationRunner>());

        var migrationEventHandler = serviceProvider.GetService<IMigrationEventHandler>();
        
        Assert.NotNull(migrationEventHandler);
        Assert.IsType<YesSqlMigrationsUpdater>(migrationEventHandler);
    }
}
