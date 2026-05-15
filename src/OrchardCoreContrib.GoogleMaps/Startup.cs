using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.Data.Migrations;
using OrchardCoreContrib.GoogleMaps.Drivers;
using OrchardCoreContrib.GoogleMaps.Migrations;
using OrchardCoreContrib.GoogleMaps.Models;
using OrchardCoreContrib.GoogleMaps.ViewModels;

namespace OrchardCoreContrib.GoogleMaps;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TemplateOptions>(o =>
        {
            o.MemberAccessStrategy.Register<GoogleMapPartViewModel>();
            o.MemberAccessStrategy.Register<GoogleMapsSettingsViewModel>();
        });

        services.AddScoped<INavigationProvider, AdminMenu>();
        services.AddScoped<IPermissionProvider, Permissions>();

        services.AddTransient<IMigration, CreateGoogleMapPart>();
        services.AddScoped<IDisplayDriver<ISite>, GoogleMapsSettingsDisplayDriver>();

        services.AddContentPart<GoogleMapPart>()
            .UseDisplayDriver<GoogleMapPartDisplayDriver>();
    }
}
