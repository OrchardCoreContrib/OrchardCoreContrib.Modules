using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCoreContrib.DataLocalization.Controllers;
using OrchardCoreContrib.DataLocalization.Services;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization;

/// <summary>
/// Represents an entry point to register the page preview bar required services.
/// </summary>
public class Startup(IOptions<AdminOptions> adminOptions) : StartupBase
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<INavigationProvider, AdminMenu>();
        services.AddScoped<TranslationsManager>();
        services.AddScoped<IDataResourceStringProvider, ContentTypeResourceStringProvider>();
        services.AddScoped<IDataResourceStringProvider, ContentFieldResourceStringProvider>();

        services.AddDataLocalization();

        services.Replace(ServiceDescriptor.Singleton<IDataTranslationProvider, DataTranslationProvider>());
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "DataLocalization",
            areaName: "OrchardCoreContrib.DataLocalization",
            pattern: _adminOptions.AdminUrlPrefix + "/DataLocalization",
            defaults: new
            {
                controller = typeof(AdminController).ControllerName(),
                action = nameof(AdminController.ManageContentTypeResources)
            }
        );
    }
}
