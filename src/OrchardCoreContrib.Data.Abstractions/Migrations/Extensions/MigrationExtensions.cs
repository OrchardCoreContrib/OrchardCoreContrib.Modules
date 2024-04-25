using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Data.Migrations;

public static class MigrationExtensions
{
    public static MigrationAttribute GetMigrationInfo(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        var migrationAttribute = (MigrationAttribute)(migration.GetType()
            .GetCustomAttributes(typeof(MigrationAttribute), false))
            .SingleOrDefault();

        return migrationAttribute;
    }

    public static string GetMigrationClass(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        return migration.GetType().FullName;
    }

    public static string GetMigrationModuleId(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        var migrationClass = migration.GetMigrationClass();

        return migrationClass[..migrationClass.IndexOf(".Migrations")];
    }
}
