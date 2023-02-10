using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Users.Models;
using OrchardCoreContrib.Users.Controllers;
using OrchardCoreContrib.Users.Drivers;
using OrchardCoreContrib.Users.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Users
{
    /// <summary>
    /// Represents an entry point to register the impersonation required services.
    /// </summary>
    [Feature("OrchardCoreContrib.Users.Impersonation")]
    public class ImpersonationStartup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        /// <summary>
        /// Initializes a new instance of <see cref="ImpersonationStartup"/>.
        /// </summary>
        /// <param name="adminOptions">The <see cref="IOptions{AdminOptions}>.</param>
        public ImpersonationStartup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IDisplayDriver<User>, ImpersonationDisplayDriver>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(10);
                options.OnRefreshingPrincipal = context =>
                {
                    var impersonatorNameClaim = context.CurrentPrincipal.FindFirst(ClaimTypesExtended.ImpersonatorNameIdentifier);
                    var isImpersonatingClaim = context.CurrentPrincipal.FindFirst(ClaimTypesExtended.IsImpersonating);
                    if (impersonatorNameClaim != null && isImpersonatingClaim?.Value == "true")
                    {
                        context.NewPrincipal.Identities.First().AddClaim(impersonatorNameClaim);
                        context.NewPrincipal.Identities.First().AddClaim(isImpersonatingClaim);
                    }

                    return Task.FromResult(0);
                };
            });
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "UsersImpersonateUser",
                areaName: "OrchardCoreContrib.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/Impersonate",
                defaults: new { controller = typeof(ImpersonationController).ControllerName(), action = nameof(ImpersonationController.ImpersonateUser) }
            );
        }
    }

    /// <summary>
    /// Represents an entry point to register the user avatar required services.
    /// </summary>
    [Feature("OrchardCoreContrib.Users.Avatar")]
    public class UserAvatarStartup : StartupBase
    {
        private readonly IShellConfiguration _shellConfiguration;
        
        public UserAvatarStartup(IShellConfiguration shellConfiguration)
        {
            _shellConfiguration = shellConfiguration;
        }

        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAvatarService, AvatarService>();
            
            services.Configure<AvatarOptions>(_shellConfiguration.GetSection("OrchardCoreContrib_Users_AvatarOptions"));
        }
    }
}
