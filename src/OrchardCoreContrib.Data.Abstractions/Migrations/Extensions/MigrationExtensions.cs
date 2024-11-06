using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Provides an extension methods for <see cref="IMigration"/>.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Gets the migration information.
    /// </summary>
    /// <param name="migration">The <see cref="IMigration"/>.</param>
    public static MigrationAttribute GetMigrationInfo(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        var migrationAttribute = (MigrationAttribute)(migration.GetType()
            .GetCustomAttributes(typeof(MigrationAttribute), false))
            .SingleOrDefault();

        return migrationAttribute;
    }

    /// <summary>
    /// Gets the migration class.
    /// </summary>
    /// <param name="migration">The <see cref="IMigration"/>.</param>
    public static string GetMigrationClass(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        return migration.GetType().FullName;
    }

    /// <summary>
    /// Gets the migration module identifier.
    /// </summary>
    /// <param name="migration">The <see cref="IMigration"/>.</param>
    public static string GetMigrationModuleId(this IMigration migration)
    {
        Guard.ArgumentNotNull(migration, nameof(migration));

        var migrationClass = migration.GetMigrationClass();

        return migrationClass[..migrationClass.IndexOf(".Migrations")];
    }
}
