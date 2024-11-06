using OrchardCoreContrib.Data.Migrations;
using YesSql;
using YesSql.Sql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

/// <summary>
/// Represents a handler for YesSql migrations.
/// </summary>
public class YesSqlMigrationsUpdater(ISession session, IStore store) : IMigrationEventHandler
{
    /// <inheritdoc/>
    public Task MigratedAsync(IMigration migration) => Task.CompletedTask;

    /// <inheritdoc/>
    public async Task MigratingAsync(IMigration migration) => await SetSchemaBuilderAsync(migration);

    /// <inheritdoc/>
    public Task RollbackedAsync(IMigration migration) => Task.CompletedTask;

    /// <inheritdoc/>
    public async Task RollbackingAsync(IMigration migration) => await SetSchemaBuilderAsync(migration);

    private async Task SetSchemaBuilderAsync(IMigration migration)
    {
        if (migration.GetType().IsSubclassOf(typeof(YesSqlMigration)))
        {
            var schemaBuilder = new SchemaBuilder(store.Configuration, await session.BeginTransactionAsync());

            ((YesSqlMigration)migration).SchemaBuilder = schemaBuilder;
        }
    }
}
