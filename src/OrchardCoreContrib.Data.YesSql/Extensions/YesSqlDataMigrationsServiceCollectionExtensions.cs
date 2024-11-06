using OrchardCoreContrib.Data.Migrations;
using OrchardCoreContrib.Data.YesSql.Migrations;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Represents an extension methods to add YesSql data migrations to the service collection.
/// </summary>
public static class YesSqlDataMigrationsServiceCollectionExtensions
{
    /// <summary>
    /// Adds YesSql data migrations to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    public static IServiceCollection AddYesSqlDataMigrations(this IServiceCollection services)
    {
        services.AddScoped<IMigrationLoader, MigrationLoader>();
        services.AddScoped<IMigrationsHistory, YesSqlMigrationsHistory>();
        services.AddScoped<IMigrationRunner, YesSqlMigrationRunner>();
        services.AddScoped<IMigrationEventHandler, YesSqlMigrationsUpdater>();

        return services;
    }
}
