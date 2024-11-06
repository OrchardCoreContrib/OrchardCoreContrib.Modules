using OrchardCore.Data.Migration.Records;

namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a contract for migrations history.
/// </summary>
public interface IMigrationsHistory
{
    /// <summary>
    /// Gets the migration record.
    /// </summary>
    DataMigrationRecord DataMigrationRecord { get; }

    /// <summary>
    /// Gets the applied migrations.
    /// </summary>
    Task<IReadOnlyList<MigrationsHistoryRow>> GetAppliedMigrationsAsync();
}
