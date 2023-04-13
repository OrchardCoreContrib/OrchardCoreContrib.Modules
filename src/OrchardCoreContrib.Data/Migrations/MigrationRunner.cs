using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using YesSql;

namespace OrchardCoreContrib.Data.Migrations;

public class MigrationRunner : IMigrationRunner
{
    private readonly MigrationDictionary _migrations;
    private readonly ISession _session;
    private readonly IEnumerable<IMigrationEventHandler> _migrationEventHandlers;
    private readonly ILogger<MigrationRunner> _logger;

    private MigrationsHistory _migrationsHistory;

    public MigrationRunner(
        IMigrationLoader migrationLoader,
        ISession session,
        IEnumerable<IMigrationEventHandler> migrationEventHandlers,
        ILogger<MigrationRunner> logger)
    {
        _session = session;
        _migrationEventHandlers = migrationEventHandlers;
        _logger = logger;

        _migrations = migrationLoader.LoadMigrations();
    }

    public async Task MigrateAsync(string moduleId)
    {
        var pendingMigrations = await GetPendingMigrationsAsync();

        foreach (var migration in pendingMigrations[moduleId])
        {
            var migrationClass = migration.Migration.GetType().FullName;
            var migrationModule = migrationClass[..migrationClass.IndexOf(".Migrations")];

            _migrationsHistory.Migrations.Add(new MigrationHistoryRow
            {
                MigrationId = migration.Id,
                MigrationModule = migrationModule,
                MigrationClass = migrationClass
            });

            try
            {
                _logger.LogInformation("Running the migration '{migration}'.", migrationClass);

                await _migrationEventHandlers.InvokeAsync((handler, migration)
                    => handler.MigratingAsync(migration), migration.Migration, _logger);

                migration.Migration.Up();

                await _migrationEventHandlers.InvokeAsync((handler, migration)
                    => handler.MigratedAsync(migration), migration.Migration, _logger);
            }
            catch
            {
                await _session.CancelAsync();
            }
            finally
            {
                _session.Save(_migrationsHistory);
            }
        }
    }

    public async Task RollbackAsync(string moduleId)
    {
        var appliedMigrations = (await GetAppliedMigrationsAsync())
            .Where(m => m.MigrationClass.StartsWith(moduleId))
            .Reverse();

        foreach (var migrationRow in appliedMigrations)
        {
            var migrationRecord = _migrations
                .SingleOrDefault(m => m.Migration.GetType().FullName.Equals(migrationRow.MigrationClass));

            if (migrationRecord != null)
            {
                try
                {
                    _logger.LogInformation("Rolling back the migration '{migration}'.", migrationRow.MigrationClass);

                    migrationRecord.Migration.Down();

                    _migrationsHistory.Migrations.Remove(migrationRow);
                }
                catch
                {
                    await _session.CancelAsync();
                }
                finally
                {
                    _session.Save(_migrationsHistory);
                }
            }
        }
    }

    private async Task<IEnumerable<MigrationHistoryRow>> GetAppliedMigrationsAsync()
    {
        if (_migrationsHistory == null)
        {
            _migrationsHistory = await _session.Query<MigrationsHistory>().FirstOrDefaultAsync();

            if (_migrationsHistory == null)
            {
                _migrationsHistory = new MigrationsHistory();

                _session.Save(_migrationsHistory);
            }
        }

        return _migrationsHistory.Migrations;
    }

    private async Task<MigrationDictionary> GetPendingMigrationsAsync()
    {
        var migrations = new MigrationDictionary();

        var appliedMigrations = await GetAppliedMigrationsAsync();

        var hasAppliedMigrations = appliedMigrations.Any();
        
        foreach (var migrationRecord in _migrations)
        {
            if (!hasAppliedMigrations || !appliedMigrations.Any(m => m.MigrationClass.Equals(migrationRecord.Migration.GetType().FullName)))
            {
                var moduleId = GetMigrationModuleId(migrationRecord.Migration);

                migrations.Add(moduleId, migrationRecord);
            }
        }

        return migrations;
    }

    private static string GetMigrationModuleId(IMigration migration)
    {
        var migrationName = migration.GetType().FullName;

        return migrationName[..migrationName.IndexOf(".Migrations")];
    }
}
