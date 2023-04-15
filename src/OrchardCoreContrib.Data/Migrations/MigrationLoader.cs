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
            var migrationInfo = migration.GetMigrationInfo();
            var record = new MigrationDictionaryRecord(migrationInfo.Id, migrationInfo.Skip, migration);
            var migrationModuleId = migration.GetMigrationModuleId();

            migrations.Add(migrationModuleId, record);
        }

        return migrations;
    }
}
