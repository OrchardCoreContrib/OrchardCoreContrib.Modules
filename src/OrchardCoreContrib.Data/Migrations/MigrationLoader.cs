namespace OrchardCoreContrib.Data.Migrations;

public class MigrationLoader : IMigrationLoader
{
    private readonly IEnumerable<IMigration> _migrations;

    public MigrationLoader(IEnumerable<IMigration> migrations)
    {
        _migrations = migrations;
    }

    public MigrationDictionary LoadMigrations()
    {
        var migrations = new MigrationDictionary();
        foreach (var migration in _migrations)
        {
            var migrationId = migration.GetMigrationId();
            var migrationModuleId = migration.GetMigrationModuleId();
            var record = new MigrationDictionaryRecord(migrationId, migration);

            migrations.Add(migrationModuleId, record);
        }

        return migrations;
    }
}
