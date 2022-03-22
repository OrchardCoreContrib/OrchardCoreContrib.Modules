using System;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Html
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "OrchardCoreContrib.Html.Home",
                areaName: "OrchardCoreContrib.Html",
                pattern: "GrapesJS/Index",
                defaults: new { controller = "GrapesJS", action = "Index" }
            );
        }
    }
}
