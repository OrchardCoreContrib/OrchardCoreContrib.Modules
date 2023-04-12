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
            var migrationClass = migration.GetType().FullName;
            var moduleId = migrationClass[..migrationClass.IndexOf(".Migrations")];
            var record = new MigrationDictionaryRecord(GetMigrationId(migration), migration);

            migrations.Add(moduleId, record);
        }

        return migrations;
    }

    private static long GetMigrationId(IMigration migration)
    {
        var migrationAttribute = (MigrationAttribute)(migration.GetType()
            .GetCustomAttributes(typeof(MigrationAttribute), false))
            .SingleOrDefault();

        return migrationAttribute?.Id ?? 0;

    }
}
