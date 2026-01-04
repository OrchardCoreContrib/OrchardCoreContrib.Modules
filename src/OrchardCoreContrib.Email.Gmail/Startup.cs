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
using OrchardCoreContrib.Email.Gmail.Controllers;
using OrchardCoreContrib.Email.Gmail.Drivers;
using OrchardCoreContrib.Email.Gmail.Services;

namespace OrchardCoreContrib.Email.Gmail;

/// <summary>
/// Represensts a startup point to register the required services by Gmail mailing module.
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
        services.AddScoped<IDisplayDriver<ISite>, GmailSettingsDisplayDriver>();
        services.AddScoped<INavigationProvider, AdminMenu>();

        services.AddTransient<IConfigureOptions<GmailSettings>, GmailSettingsConfiguration>();
        services.AddScoped<ISmtpService, GmailService>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "GmailIndex",
            areaName: "OrchardCoreContrib.Email.Gmail",
            pattern: _adminOptions.AdminUrlPrefix + "/Gmail/Index",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
        );
    }
}
