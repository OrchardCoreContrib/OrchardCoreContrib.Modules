using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Module1.Migrations;
using OrchardCoreContrib.Module2.Migrations;

namespace OrchardCoreContrib.Data.Migrations.Tests;

public class MigrationLoaderTests
{
    [Fact]
    public void ShouldLoadMigrationsFromDI()
    {
        // Arrange
        var migrationLoader = GetMigrationLoader();

        // Act
        var migrationsDictionary = migrationLoader.LoadMigrations();

        // Assert
        Assert.NotNull(migrationsDictionary);
        Assert.Equal(4, migrationsDictionary.Count());
        Assert.Contains(migrationsDictionary, r => r.Migration.GetType().Name == "Migration1");
        Assert.Contains(migrationsDictionary, r => r.Migration.GetType().Name == "Migration2");
        Assert.Contains(migrationsDictionary, r => r.Migration.GetType().Name == "Migration3");
    }

    [Theory]
    [InlineData("OrchardCoreContrib.Module1", 3)]
    [InlineData("OrchardCoreContrib.Module2", 1)]
    public void LoaderShouldRetrieveMigrationsPerModuleId(string moduleId, int expectedMigrationsCount)
    {
        // Arrange
        var migrationLoader = GetMigrationLoader();

        // Act
        var migrationsDictionary = migrationLoader.LoadMigrations();

        // Assert
        Assert.Equal(expectedMigrationsCount, migrationsDictionary[moduleId].Count());
    }

    private static IMigrationLoader GetMigrationLoader()
    {
        var services = new ServiceCollection();

        services.AddScoped<IMigration, Migration1>();
        services.AddScoped<IMigration, Migration2>();
        services.AddScoped<IMigration, Migration3>();
        services.AddScoped<IMigration, Migration4>();
        services.AddScoped<IMigrationLoader, MigrationLoader>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetService<IMigrationLoader>();
    }
}