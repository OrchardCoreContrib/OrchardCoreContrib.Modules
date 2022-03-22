using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.ResourceManagement;
using OrchardCoreContrib.ContentPreview.Controllers;
using System;

namespace OrchardCoreContrib.ContentPreview
{
    /// <summary>
    /// Represents an entry point to register the page preview bar required services.
    /// </summary>
    [Feature("OrchardCoreContrib.ContentPreview.PagePreviewBar")]
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "PreviewContent",
                areaName: "OrchardCoreContrib.ContentPreview",
                pattern: "/Preview/{*page}",
                defaults: new { controller = typeof(PreviewController).ControllerName(), action = nameof(PreviewController.Index) }
            );

            app.UsePagePreview();
        }
    }
}
