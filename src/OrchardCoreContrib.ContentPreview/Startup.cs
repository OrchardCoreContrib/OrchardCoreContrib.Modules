using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Environment.Shell;
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
        private readonly AdminOptions _adminOptions;

        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="adminOptions">The <see cref="IOptions<AdminOptions>"/>.</param>
        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {

        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var shellFeaturesManager = serviceProvider.GetService<IShellFeaturesManager>();
            var shellSettings = serviceProvider.GetService<ShellSettings>();

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;
                if (path.StartsWith($"/{_adminOptions.AdminUrlPrefix}", StringComparison.OrdinalIgnoreCase))
                {
                    await next();

                    return;
                }

                var featureEnabled = shellFeaturesManager
                    .GetEnabledExtensionsAsync().Result
                    .Any(f => f.Id == "OrchardCoreContrib.ContentPreview");

                if (!featureEnabled)
                {
                    await next();

                    return;
                }

                var isPreview = context.Request.Query.ContainsKey("preview");

                if (!path.Contains("preview") && !isPreview)
                {
                    context.Response.Redirect($"/{shellSettings.RequestUrlPrefix}/preview{path}");
                }

                await next();
            });

            routes.MapAreaControllerRoute(
                name: "PreviewContent",
                areaName: "OrchardCoreContrib.ContentPreview",
                pattern: "/Preview/{*page}",
                defaults: new { controller = typeof(PreviewController).ControllerName(), action = nameof(PreviewController.Index) }
            );
        }
    }
}
