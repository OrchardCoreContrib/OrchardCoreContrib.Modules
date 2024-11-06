namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a contract for running a migration.
/// </summary>
public interface IMigrationRunner
{
    /// <summary>
    /// Applies a migration with a given id.
    /// </summary>
    /// <param name="moduleId">The module identifier.</param>
    /// <param name="targetMigrationId">The migration identifier.</param>
    Task MigrateAsync(string moduleId, long targetMigrationId = 0);

    /// <summary>
    /// Rolls back a migration with a given id.
    /// </summary>
    /// <param name="moduleId">The module identifier.</param>
    /// <param name="targetMigrationId">The migration identifier.</param>
    Task RollbackAsync(string moduleId, long targetMigrationId = 0);
}
