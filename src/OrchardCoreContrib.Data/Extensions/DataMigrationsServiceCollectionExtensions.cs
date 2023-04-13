using OrchardCoreContrib.Data.Migrations;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataMigrationsServiceCollectionExtensions
{
    public static IServiceCollection AddDataMigrations(this IServiceCollection services)
    {
        services.AddScoped<IMigrationLoader, MigrationLoader>();
        services.AddScoped<IMigrationRunner, MigrationRunner>();

        return services;
    }
}
