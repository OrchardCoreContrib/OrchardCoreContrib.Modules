namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationRunner
{
    Task MigrateAsync(string moduleId);

    Task RollbackAsync(string moduleId);
}
