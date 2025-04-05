using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Users.Models;
using OrchardCoreContrib.UserGroups.Drivers;
using OrchardCoreContrib.UserGroups.Models;
using OrchardCoreContrib.UserGroups.Services;

namespace OrchardCoreContrib.UserGroups;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddDataMigration<Migrations>();
        services.AddPermissionProvider<Permissions>();
        services.AddNavigationProvider<AdminMenu>();

        services.AddContentPart<UserGroupsListPart>()
            .UseDisplayDriver<UserGroupsListPartDisplayDriver>();

        services.AddScoped<IDisplayDriver<User>, UserGroupsDisplayDriver>();
        services.AddScoped<UserGroupDocument>();
        services.AddScoped<UserGroupsManager>();
    }
}

