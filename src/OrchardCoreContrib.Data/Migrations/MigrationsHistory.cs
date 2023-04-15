namespace OrchardCoreContrib.Data.Migrations;

public class MigrationsHistory
{
    public MigrationsHistory()
    {
        Migrations = new List<MigrationsHistoryRow>();
    }

    public IList<MigrationsHistoryRow> Migrations { get; }
}
