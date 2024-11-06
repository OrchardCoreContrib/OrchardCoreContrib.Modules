using OrchardCore.Data.Migration.Records;
using YesSql;

namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a history for YesSql migrations.
/// </summary>
public class YesSqlMigrationsHistory(ISession session) : IMigrationsHistory
{
    private DataMigrationRecord _dataMigrationRecord;

    /// <inheritdoc/>
    public DataMigrationRecord DataMigrationRecord => _dataMigrationRecord;

    /// <inheritdoc/>
    public async Task<IReadOnlyList<MigrationsHistoryRow>> GetAppliedMigrationsAsync()
    {
        if (_dataMigrationRecord == null)
        {
            _dataMigrationRecord = await session
                .Query<DataMigrationRecord>()
                .FirstOrDefaultAsync();

            if (_dataMigrationRecord == null)
            {
                _dataMigrationRecord = new DataMigrationRecord();
                await session.SaveAsync(_dataMigrationRecord);
            }
        }

        return _dataMigrationRecord.DataMigrations
            .OfType<MigrationsHistoryRow>()
            .ToList();
    }
}
