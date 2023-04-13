namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationEventHandler
{
    Task MigratingAsync(IMigration migration);
    
    Task MigratedAsync(IMigration migration);

    Task RollbackingAsync(IMigration migration);

    Task RollbackedAsync(IMigration migration);
}
