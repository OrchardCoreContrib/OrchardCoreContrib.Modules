using OrchardCore.Data.Migration.Records;

namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a row for migrations history.
/// </summary>
public class MigrationsHistoryRow : DataMigration
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public long Id { get; set; }
}