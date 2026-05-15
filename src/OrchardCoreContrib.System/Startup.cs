using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.HealthChecks;
using OrchardCoreContrib.System.Controllers;
using OrchardCoreContrib.System.Drivers;
using OrchardCoreContrib.System.HealthChecks;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System;

/// <summary>
/// Provides application startup configuration for the System module, including service registrations
/// and route mappings.
/// </summary>
public class Startup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<INavigationProvider, AdminMenu>();

        services.AddSingleton<SystemInformation>();
    }

    /// <inheritdoc/>
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

/// <summary>
/// Configures services and routes required for the system updates feature within an Orchard Core application.
/// </summary>
[Feature("OrchardCoreContrib.System.Updates")]
public class UpdatesStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISystemUpdateService, SystemUpdateService>();

        services.AddScoped<INavigationProvider, UpdatesAdminMenu>();

        services.AddHealthChecks()
            .AddSystemUpdatesCheck();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "SystemUpdates",
            areaName: "OrchardCoreContrib.System",
            pattern: "Admin/Updates",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Updates) }
        );

        var healthChecksOptions = serviceProvider.GetService<IOptions<HealthChecksOptions>>().Value;

        routes.MapHealthChecks($"{healthChecksOptions.Url}/updates", new HealthCheckOptions
        {
            Predicate = r => r.Name == SystemUpdatesHealthCheck.Name
        });
    }
}

/// <summary>
/// Configures services and routes required for the system maintenance feature within an Orchard Core application.
/// </summary>
[Feature("OrchardCoreContrib.System.Maintenance")]
public class MaintenaceStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPermissionProvider, Permissions>();
        services.AddScoped<IDisplayDriver<ISite>, SystemSettingsDisplayDriver>();
        services.AddScoped<INavigationProvider, MaintenanceAdminMenu>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        => app.UseMaintenanceRedirect();
}
