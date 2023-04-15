using OrchardCore.Data.Migration.Records;
using YesSql;

namespace OrchardCoreContrib.Data.Migrations;

public class YesSqlMigrationsHistory : IMigrationsHistory
{
    private readonly ISession _session;

    private DataMigrationRecord _dataMigrationRecord;

    public YesSqlMigrationsHistory(ISession session)
    {
        _session = session;
    }

    public DataMigrationRecord DataMigrationRecord => _dataMigrationRecord;

    public async Task<IReadOnlyList<MigrationsHistoryRow>> GetAppliedMigrationsAsync()
    {
        if (_dataMigrationRecord == null)
        {
            _dataMigrationRecord = await _session
                .Query<DataMigrationRecord>()
                .FirstOrDefaultAsync();

            if (_dataMigrationRecord == null)
            {
                _dataMigrationRecord = new DataMigrationRecord();
                _session.Save(_dataMigrationRecord);
            }
        }

        return _dataMigrationRecord.DataMigrations
            .OfType<MigrationsHistoryRow>()
            .ToList();
    }
}
