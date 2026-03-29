using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.ResourceManagement;
using OrchardCoreContrib.Html.Controllers;

namespace OrchardCoreContrib.Html;

[Feature("OrchardCoreContrib.Html.GrapesJS")]
public class Startup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "GrapesJSEditor",
            areaName: "OrchardCoreContrib.Html",
            pattern: "GrapesJSEditor",
            defaults: new
            {
                controller = typeof(AdminController).ControllerName(),
                action = nameof(AdminController.Index)
            }
        );
    }
}
