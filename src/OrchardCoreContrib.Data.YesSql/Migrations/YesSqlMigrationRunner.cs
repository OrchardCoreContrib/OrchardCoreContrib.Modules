using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCoreContrib.Data.Migrations;
using YesSql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

/// <summary>
/// Represents a runner for YesSql migrations.
/// </summary>
public class YesSqlMigrationRunner(
    IMigrationLoader migrationLoader,
    IMigrationsHistory migrationsHistory,
    ISession session,
    IEnumerable<IMigrationEventHandler> migrationEventHandlers,
    ILogger<YesSqlMigrationRunner> logger) : IMigrationRunner
{
    private readonly MigrationDictionary _migrations = migrationLoader.LoadMigrations();

    /// <inheritdoc/>
    public async Task MigrateAsync(string moduleId, long targetMigrationId = 0)
    {
        var pendingMigrations = await GetPendingMigrationsAsync();

        foreach (var migrationRecord in pendingMigrations[moduleId])
        {
            var migrationClass = migrationRecord.Migration.GetMigrationClass();

            if (migrationRecord.Skip)
            {
                logger.LogWarning("Skipping the migration '{migration}'.", migrationClass);

                continue;
            }

            migrationsHistory.DataMigrationRecord.DataMigrations.Add(new MigrationsHistoryRow
            {
                Id = migrationRecord.Id,
                DataMigrationClass = migrationClass
            });

            try
            {
                logger.LogInformation("Running the migration '{migration}'.", migrationClass);

                await migrationEventHandlers.InvokeAsync((handler, migration)
                    => handler.MigratingAsync(migration), migrationRecord.Migration, logger);

                migrationRecord.Migration.Up();

                await migrationEventHandlers.InvokeAsync((handler, migration)
                    => handler.MigratedAsync(migration), migrationRecord.Migration, logger);
            }
            catch
            {
                await session.CancelAsync();
            }
            finally
            {
                await session.SaveAsync(migrationsHistory.DataMigrationRecord);
            }

            if (migrationRecord.Id == targetMigrationId)
            {
                break;
            }
        }
    }

    /// <inheritdoc/>
    public async Task RollbackAsync(string moduleId, long targetMigrationId = 0)
    {
        var appliedMigrations = (await migrationsHistory.GetAppliedMigrationsAsync())
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
                    logger.LogInformation("Rolling back the migration '{migration}'.", migrationRow.DataMigrationClass);

                    await migrationEventHandlers.InvokeAsync((handler, migration)
                        => handler.RollbackingAsync(migration), migrationRecord.Migration, logger);

                    migrationRecord.Migration.Down();

                    await migrationEventHandlers.InvokeAsync((handler, migration)
                        => handler.RollbackedAsync(migration), migrationRecord.Migration, logger);

                    migrationsHistory.DataMigrationRecord.DataMigrations.Remove(migrationRow);
                }
                catch
                {
                    await session.CancelAsync();
                }
                finally
                {
                    await session.SaveAsync(migrationsHistory.DataMigrationRecord);
                }

                if (migrationRecord.Id == targetMigrationId)
                {
                    break;
                }
            }
        }
    }

    private async Task<MigrationDictionary> GetPendingMigrationsAsync()
    {
        var migrations = new MigrationDictionary();

        var appliedMigrations = await migrationsHistory.GetAppliedMigrationsAsync();

        var hasAppliedMigrations = appliedMigrations.Any();
        
        foreach (var migrationRecord in _migrations)
        {
            if (!hasAppliedMigrations || !appliedMigrations.Any(m => m.DataMigrationClass.Equals(migrationRecord.Migration.GetType().FullName)))
            {
                var moduleId = migrationRecord.Migration.GetMigrationModuleId();

                migrations.Add(moduleId, migrationRecord);
            }
        }

        return migrations;
    }
}
