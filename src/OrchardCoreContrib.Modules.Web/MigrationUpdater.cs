using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Data.Migrations;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Modules.Web;

public class MigrationUpdater : IFeatureEventHandler
{
    private readonly IMigrationRunner _migrationRunner;

    public MigrationUpdater(IMigrationRunner migrationRunner)
    {
        _migrationRunner = migrationRunner;
    }

    public Task DisabledAsync(IFeatureInfo feature) => Task.CompletedTask;

    public Task DisablingAsync(IFeatureInfo feature) => Task.CompletedTask;

    public Task EnabledAsync(IFeatureInfo feature) => Task.CompletedTask;

    public Task EnablingAsync(IFeatureInfo feature) => Task.CompletedTask;

    public async Task InstalledAsync(IFeatureInfo feature)
        => await _migrationRunner.MigrateAsync(feature.Extension.Manifest.ModuleInfo.Id);

    public Task InstallingAsync(IFeatureInfo feature) => Task.CompletedTask;

    public async Task UninstalledAsync(IFeatureInfo feature)
        => await _migrationRunner.RollbackAsync(feature.Extension.Manifest.ModuleInfo.Id);

    public Task UninstallingAsync(IFeatureInfo feature) => Task.CompletedTask;

}
