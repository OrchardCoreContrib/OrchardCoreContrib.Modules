namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationRunner
{
    Task MigrateAsync(string moduleId, long targetMigrationId = 0);

    Task RollbackAsync(string moduleId, long targetMigrationId = 0);
}
