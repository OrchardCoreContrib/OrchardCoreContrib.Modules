using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Email;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.Yahoo.Controllers;
using OrchardCoreContrib.Email.Yahoo.Drivers;
using OrchardCoreContrib.Email.Yahoo.Services;

namespace OrchardCoreContrib.Email.Yahoo;

/// <summary>
/// Represensts a startup point to register the required services by Yahoo mailing module.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="Startup"/>.
/// </remarks>
/// <param name="adminOptions">The <see cref="IOptions{AdminOptions}>.</param>
public class Startup(IOptions<AdminOptions> adminOptions) : StartupBase
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPermissionProvider, Permissions>();
        services.AddScoped<IDisplayDriver<ISite>, YahooSettingsDisplayDriver>();
        services.AddScoped<INavigationProvider, AdminMenu>();

        services.AddTransient<IConfigureOptions<YahooSettings>, YahooSettingsConfiguration>();
        services.AddScoped<ISmtpService, YahooService>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "YahooIndex",
            areaName: "OrchardCoreContrib.Email.Yahoo",
            pattern: _adminOptions.AdminUrlPrefix + "/Yahoo/Index",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
        );
    }
}
