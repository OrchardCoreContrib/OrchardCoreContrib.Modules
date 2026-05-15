using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCoreContrib.ContentLocalization.Controllers;
using OrchardCoreContrib.ContentLocalization.Services;

namespace OrchardCoreContrib.ContentLocalization;

[Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
public class LocalizationMatrixStartup(IOptions<AdminOptions> adminOptions) : StartupBase
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransliteration();

        services.AddScoped<INavigationProvider, AdminMenu>();
        services.AddScoped<IContentLocalizationManager, DefaultContentLocalizationManager>();
        services.AddScoped<ITransliterationService, TransliterationService>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "ContentLocalization.LocalizationMatrix",
            areaName: "OrchardCoreContrib.ContentLocalization",
            pattern: _adminOptions.AdminUrlPrefix + "/LocalizationMatrix",
            defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.LocalizationMatrix) }
        );
    }
}
