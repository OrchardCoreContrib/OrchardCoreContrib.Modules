namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a record for a migration dictionary.
/// </summary>
public class MigrationDictionaryRecord(long id, bool skip, IMigration migration)
{
    private readonly Lazy<IMigration> _migration = new(migration);

    /// <summary>
    /// Gets the migration identifier.
    /// </summary>
    public long Id { get; } = id;

    /// <summary>
    /// Checks if the migration should be skipped.
    /// </summary>
    public bool Skip { get; } = skip;

    /// <summary>
    /// Gets the migration.
    /// </summary>
    public IMigration Migration => _migration.Value;
}
