namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationLoader
{
    MigrationDictionary LoadMigrations();
}
