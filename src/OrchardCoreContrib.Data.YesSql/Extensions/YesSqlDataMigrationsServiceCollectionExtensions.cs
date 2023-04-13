using OrchardCoreContrib.Data.Migrations;
using OrchardCoreContrib.Data.YesSql.Migrations;

namespace Microsoft.Extensions.DependencyInjection;

public static class YesSqlDataMigrationsServiceCollectionExtensions
{
    public static IServiceCollection AddYesSqlDataMigrations(this IServiceCollection services)
    {
        services.AddDataMigrations();

        services.AddScoped<IMigrationEventHandler, YesSqlMigrationsUpdater>();

        return services;
    }
}
