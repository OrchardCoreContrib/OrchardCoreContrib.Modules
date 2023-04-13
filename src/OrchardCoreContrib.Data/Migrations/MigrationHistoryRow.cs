namespace OrchardCoreContrib.Data.Migrations;

public class MigrationHistoryRow
{
    public long MigrationId { get; set; }

    public string MigrationModule { get; set; }

    public string MigrationClass { get; set; }
}