using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCoreContrib.ViewCount.Drivers;
using OrchardCoreContrib.ViewCount.Models;
using OrchardCoreContrib.ViewCount.Services;

namespace OrchardCoreContrib.ViewCount;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddContentPart<ViewCountPart>()
            .UseDisplayDriver<ViewCountPartDisplayDriver>();

        services.AddDataMigration<Migrations>();
        services.AddScoped<IViewCountService, ViewCountService>();
    }
}

