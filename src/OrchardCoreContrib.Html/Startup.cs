using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCoreContrib.Html.Controllers;

namespace OrchardCoreContrib.Html
{
    [Feature("OrchardCoreContrib.Html.GrapesJS")]
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "GrapesJSEditor",
                areaName: "OrchardCoreContrib.Html",
                pattern: "GrapesJSEditor",
                defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
            );
        }
    }
}
