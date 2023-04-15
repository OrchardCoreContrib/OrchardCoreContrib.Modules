namespace OrchardCoreContrib.Data.Migrations;

public static class MigrationExtensions
{
    public static long GetMigrationId(this IMigration migration)
    {
        if (migration is null)
        {
            throw new ArgumentNullException(nameof(migration));
        }

        var migrationAttribute = (MigrationAttribute)(migration.GetType()
            .GetCustomAttributes(typeof(MigrationAttribute), false))
            .SingleOrDefault();

        return migrationAttribute?.Id ?? 0;
    }

    public static string GetMigrationClass(this IMigration migration)
    {
        if (migration is null)
        {
            throw new ArgumentNullException(nameof(migration));
        }

        return migration.GetType().FullName;
    }

    public static string GetMigrationModuleId(this IMigration migration)
    {
        if (migration is null)
        {
            throw new ArgumentNullException(nameof(migration));
        }

        var migrationClass = migration.GetMigrationClass();

        return migrationClass[..migrationClass.IndexOf(".Migrations")];
    }
}
