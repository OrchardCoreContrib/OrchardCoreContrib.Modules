using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCoreContrib.Data.Migrations;
using YesSql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

public class YesSqlMigrationsRunner : IMigrationsRunner
{
    private readonly MigrationDictionary _migrations;
    private readonly IMigrationsHistory _migrationsHistory;
    private readonly ISession _session;
    private readonly IEnumerable<IMigrationEventHandler> _migrationEventHandlers;
    private readonly ILogger<YesSqlMigrationsRunner> _logger;

    public YesSqlMigrationsRunner(
        IMigrationLoader migrationLoader,
        IMigrationsHistory migrationsHistory,
        ISession session,
        IEnumerable<IMigrationEventHandler> migrationEventHandlers,
        ILogger<YesSqlMigrationsRunner> logger)
    {
        _migrationsHistory = migrationsHistory;
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
            var migrationClass = migration.Migration.GetMigrationClass();

            _migrationsHistory.DataMigrationRecord.DataMigrations.Add(new MigrationsHistoryRow
            {
                Id = migration.Id,
                DataMigrationClass = migrationClass
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
                _session.Save(_migrationsHistory.DataMigrationRecord);
            }
        }
    }

    public async Task RollbackAsync(string moduleId)
    {
        var appliedMigrations = (await _migrationsHistory.GetAppliedMigrationsAsync())
            .Where(m => m.DataMigrationClass.StartsWith(moduleId))
            .Reverse();

        foreach (var migrationRow in appliedMigrations)
        {
            var migrationRecord = _migrations
                .SingleOrDefault(m => m.Migration.GetType().FullName.Equals(migrationRow.DataMigrationClass));

            if (migrationRecord != null)
            {
                try
                {
                    _logger.LogInformation("Rolling back the migration '{migration}'.", migrationRow.DataMigrationClass);

                    await _migrationEventHandlers.InvokeAsync((handler, migration)
                        => handler.RollbackingAsync(migration), migrationRecord.Migration, _logger);

                    migrationRecord.Migration.Down();

                    await _migrationEventHandlers.InvokeAsync((handler, migration)
                        => handler.RollbackedAsync(migration), migrationRecord.Migration, _logger);

                    _migrationsHistory.DataMigrationRecord.DataMigrations.Remove(migrationRow);
                }
                catch
                {
                    await _session.CancelAsync();
                }
                finally
                {
                    _session.Save(_migrationsHistory.DataMigrationRecord);
                }
            }
        }
    }

    private async Task<MigrationDictionary> GetPendingMigrationsAsync()
    {
        var migrations = new MigrationDictionary();

        var appliedMigrations = await _migrationsHistory.GetAppliedMigrationsAsync();

        var hasAppliedMigrations = appliedMigrations.Any();
        
        foreach (var migrationRecord in _migrations)
        {
            if (!hasAppliedMigrations || !appliedMigrations.Any(m => m.DataMigrationClass.Equals(migrationRecord.Migration.GetType().FullName)))
            {
                if (migrationRecord.Skip)
                {
                    continue;
                }

                var moduleId = migrationRecord.Migration.GetMigrationModuleId();

                migrations.Add(moduleId, migrationRecord);
            }
        }

        return migrations;
    }
}
