using OrchardCore.Data.Migration.Records;

namespace OrchardCoreContrib.Data.Migrations;

public interface IMigrationsHistory
{
    DataMigrationRecord DataMigrationRecord { get; }

    Task<IReadOnlyList<MigrationsHistoryRow>> GetAppliedMigrationsAsync();
}
