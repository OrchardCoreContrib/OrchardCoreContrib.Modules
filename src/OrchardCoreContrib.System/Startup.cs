using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCoreContrib.System.Controllers;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<INavigationProvider, AdminMenu>();

        services.AddSingleton<SystemInformation>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "SystemInfo",
            areaName: "OrchardCoreContrib.System",
            pattern: "Admin/About",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.About) }
        );
    }
}

[Feature("OrchardCoreContrib.System.Updates")]
public class UpdatesStartup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<INavigationProvider, UpdatesAdminMenu>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "SystemUpdates",
            areaName: "OrchardCoreContrib.System",
            pattern: "Admin/Updates",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Updates) }
        );
    }
}
