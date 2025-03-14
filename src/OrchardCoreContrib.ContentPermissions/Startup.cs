using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCoreContrib.ContentPermissions.Drivers;
using OrchardCoreContrib.ContentPermissions.Migrations;
using OrchardCoreContrib.ContentPermissions.Models;
using OrchardCoreContrib.ContentPermissions.Services;

namespace OrchardCoreContrib.ContentPermissions;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddDataMigration<ContentPermissionsPartMigration>();

        services.AddContentPart<ContentPermissionsPart>()
            .UseDisplayDriver<ContentPermissionsPartDisplayDriver>();

        services.AddScoped<IContentTypePartDefinitionDisplayDriver, ContentPermissionsPartSettingsDisplayDriver>();
        services.AddScoped<IContentPermissionsService, ContentPermissionsServices>();
    }
}
