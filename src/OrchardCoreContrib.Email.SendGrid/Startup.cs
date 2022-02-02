using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Email;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.SendGrid.Controllers;
using OrchardCoreContrib.Email.SendGrid.Drivers;
using OrchardCoreContrib.Email.SendGrid.Services;
using System;

namespace OrchardCoreContrib.Email.SendGrid
{
    /// <summary>
    /// Represensts a startup point to register the required services by SendGrid mailing module.
    /// </summary>
    public class Startup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="adminOptions">The <see cref="IOptions{AdminOptions}>.</param>
        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, SendGridSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddTransient<IConfigureOptions<SendGridSettings>, SendGridSettingsConfiguration>();
            services.AddScoped<ISmtpService, SendGridService>();
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "GmailIndex",
                areaName: "OrchardCoreContrib.Email.SendGrid",
                pattern: _adminOptions.AdminUrlPrefix + "/SendGrid/Index",
                defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
            );
        }
    }
}
