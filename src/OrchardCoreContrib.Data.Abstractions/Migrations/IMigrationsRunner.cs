namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationsRunner
{
    Task MigrateAsync(string moduleId);

    Task RollbackAsync(string moduleId);
}
