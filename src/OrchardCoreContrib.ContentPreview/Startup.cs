﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCoreContrib.ContentPreview.Controllers;

namespace OrchardCoreContrib.ContentPreview
{
    /// <summary>
    /// Represents an entry point to register the page preview bar required services.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {

        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UsePagePreview();

            routes.MapAreaControllerRoute(
                name: "PreviewContent",
                areaName: "OrchardCoreContrib.ContentPreview",
                pattern: "/Preview/{*page}",
                defaults: new { controller = typeof(PreviewController).ControllerName(), action = nameof(PreviewController.Index) }
            );
        }
    }
}
