using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.Gdpr.Controllers;
using OrchardCoreContrib.Gdpr.Drivers;

namespace OrchardCoreContrib.Gdpr
{
    /// <summary>
    /// Represensts a startup point to register the required services by GDPR module.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, GdprSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

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
