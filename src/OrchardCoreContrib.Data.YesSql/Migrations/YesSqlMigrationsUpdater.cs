using OrchardCoreContrib.Data.Migrations;
using YesSql;
using YesSql.Sql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

public class YesSqlMigrationsUpdater : IMigrationEventHandler
{
    private readonly ISession _session;
    private readonly IStore _store;

    public YesSqlMigrationsUpdater(ISession session, IStore store)
    {
        _session = session;
        _store = store;
    }

    public Task MigratedAsync(IMigration migration) => Task.CompletedTask;

    public async Task MigratingAsync(IMigration migration) => await SetSchemaBuilderAsync(migration);

    public Task RollbackedAsync(IMigration migration) => Task.CompletedTask;

    public async Task RollbackingAsync(IMigration migration) => await SetSchemaBuilderAsync(migration);

    private async Task SetSchemaBuilderAsync(IMigration migration)
    {
        if (migration.GetType().IsSubclassOf(typeof(YesSqlMigration)))
        {
            var schemaBuilder = new SchemaBuilder(_store.Configuration, await _session.BeginTransactionAsync());

            ((YesSqlMigration)migration).SchemaBuilder = schemaBuilder;
        }
    }
}
