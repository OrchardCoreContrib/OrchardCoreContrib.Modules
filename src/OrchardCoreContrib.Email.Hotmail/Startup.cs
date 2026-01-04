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
using OrchardCoreContrib.Email.Hotmail.Controllers;
using OrchardCoreContrib.Email.Hotmail.Drivers;
using OrchardCoreContrib.Email.Hotmail.Services;

namespace OrchardCoreContrib.Email.Hotmail;

/// <summary>
/// Represensts a startup point to register the required services by Hotmail mailing module.
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
        services.AddScoped<IDisplayDriver<ISite>, HotmailSettingsDisplayDriver>();
        services.AddScoped<INavigationProvider, AdminMenu>();

        services.AddTransient<IConfigureOptions<HotmailSettings>, HotmailSettingsConfiguration>();
        services.AddScoped<ISmtpService, HotmailService>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "HotmailIndex",
            areaName: "OrchardCoreContrib.Email.Hotmail",
            pattern: _adminOptions.AdminUrlPrefix + "/Hotmail/Index",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
        );
    }
}
