using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.Sms.Azure.Controllers;
using OrchardCoreContrib.Sms.Azure.Drivers;
using OrchardCoreContrib.Sms.Azure.Services;
using OrchardCoreContrib.Validation;

namespace OrchardCoreContrib.Sms.Azure;

public class Startup(IOptions<AdminOptions> adminOptions) : StartupBase
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IPhoneNumberValidator, PhoneNumberValidator>();
        services.AddTransient<ISmsService, AzureSmsService>();

        services.AddScoped<IPermissionProvider, AzureSmsPermissionProvider>();
        services.AddScoped<INavigationProvider, AdminMenu>();
        services.AddScoped<IDisplayDriver<ISite>, AzureSmsSettingsDisplayDriver>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "AzureSmsTest",
            areaName: "OrchardCoreContrib.Sms.Azure",
            pattern: _adminOptions.AdminUrlPrefix + "/AzureSms",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
        );
    }
}
