namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a contract for a migration.
/// </summary>
public interface IMigration
{
    /// <summary>
    /// Apply the migration.
    /// </summary>
    void Up();

    /// <summary>
    /// Rollback the migration.
    /// </summary>
    void Down();
}
