namespace OrchardCoreContrib.Data.Migrations;

public class MigrationsHistory
{
    public MigrationsHistory()
    {
        Migrations = new List<MigrationHistoryRow>();
    }

    public List<MigrationHistoryRow> Migrations { get; }
}
