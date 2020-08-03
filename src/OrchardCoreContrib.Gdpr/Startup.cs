using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCoreContrib.Gdpr.Controllers;

namespace OrchardCoreContrib.Gdpr
{
    /// <summary>
    /// Represensts a startup point to register the required services by Yahoo mailing module.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(CookieConsentFilter));
            });
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder.UseCookiePolicy();

            routes.MapAreaControllerRoute(
                name: "GdprPrivacy",
                areaName: "OrchardCoreContrib.Gdpr",
                pattern: "/Privacy",
                defaults: new { controller = typeof(HomeController).ControllerName(), action = nameof(HomeController.Privacy) }
            );
        }
    }
}
