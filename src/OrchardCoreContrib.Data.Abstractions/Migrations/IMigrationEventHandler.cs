namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Representst a contract for a migration event handler.
/// </summary>
public interface IMigrationEventHandler
{
    /// <summary>
    /// An event that is triggered before a migration is applied.
    /// </summary>
    /// <param name="migration"><see cref="IMigration"/>.</param>
    Task MigratingAsync(IMigration migration);

    /// <summary>
    /// An event that is triggered after a migration is applied.
    /// </summary>
    /// <param name="migration"><see cref="IMigration"/>.</param>
    Task MigratedAsync(IMigration migration);

    /// <summary>
    /// An event that is triggered before a migration is rolled back.
    /// </summary>
    /// <param name="migration"><see cref="IMigration"/>.</param>
    Task RollbackingAsync(IMigration migration);

    /// <summary>
    /// An event that is triggered after a migration is rolled back.
    /// </summary>
    /// <param name="migration"><see cref="IMigration"/>.</param>
    Task RollbackedAsync(IMigration migration);
}
