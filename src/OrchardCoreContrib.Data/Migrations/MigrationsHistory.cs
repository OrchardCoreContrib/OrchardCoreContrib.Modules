namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents the migrations history.
/// </summary>
public class MigrationsHistory
{
    public MigrationsHistory() => Migrations = [];

    public IList<MigrationsHistoryRow> Migrations { get; }
}
