namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a contract for loading migrations.
/// </summary>
public interface IMigrationLoader
{
    /// <summary>
    /// Loads the migrations.
    /// </summary>
    MigrationDictionary LoadMigrations();
}
