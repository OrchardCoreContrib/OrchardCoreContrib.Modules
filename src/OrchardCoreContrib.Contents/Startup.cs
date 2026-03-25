using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;
using OrchardCoreContrib.Contents.Drivers;
using OrchardCoreContrib.Contents.Indexes;
using OrchardCoreContrib.Contents.Services;

namespace OrchardCoreContrib.Contents;

[Feature("OrchardCoreContrib.Contents.ShareDraftContent")]
public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBackgroundTask, SharedDraftCleanupTask>();

        services.AddDataMigration<Migrations>();

        services.AddIndexProvider<SharedDraftLinkIndexProvider>();

        services.AddScoped<IContentDisplayDriver, ShareDraftContentDriver>();
        services.AddScoped<ISharedDraftLinkService, SharedDraftLinkService>();

        services.AddPermissionProvider<Permissions>();
    }
}
