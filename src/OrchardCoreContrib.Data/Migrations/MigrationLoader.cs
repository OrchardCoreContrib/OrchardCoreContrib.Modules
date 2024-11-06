namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a migration loader.
/// </summary>
public class MigrationLoader(IEnumerable<IMigration> migrations) : IMigrationLoader
{
    /// <inheritdoc/>
    public MigrationDictionary LoadMigrations()
    {
        var migrationDictionary = new MigrationDictionary();
        foreach (var migration in migrations)
        {
            var migrationInfo = migration.GetMigrationInfo();
            if (migrationInfo is null)
            {
                continue;
            }

            var record = new MigrationDictionaryRecord(migrationInfo.Id, migrationInfo.Skip, migration);
            var migrationModuleId = migration.GetMigrationModuleId();

            migrationDictionary.Add(migrationModuleId, record);
        }

        return migrationDictionary;
    }
}
