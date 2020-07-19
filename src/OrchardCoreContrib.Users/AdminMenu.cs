using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCoreContrib.Users.Controllers;
using OrchardCoreContrib.Users.Drivers;
using OrchardCoreContrib.Users.Models;

namespace OrchardCoreContrib.Users
{
    /// <summary>
    /// Represents an admin menu for the impersonation feature.
    /// </summary>
    [Feature("OrchardCore.Users.Impersonation")]
    public class AdminMenu : INavigationProvider
    {
        private readonly HttpContext _httpContext;
        private readonly ImpersonationSettings _impersonationSettings;
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer<AdminMenu>"/>.</param>
        public AdminMenu(
            IHttpContextAccessor httpContextAccessor,
            ISiteService site,
            IStringLocalizer<AdminMenu> localizer)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _impersonationSettings = site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<ImpersonationSettings>();
            S = localizer;
        }

        /// <inheritdoc/>
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Security"], security => security
                    .Add(S["Settings"], settings => settings
                        .Add(S["Impersonation"], S["Impersonation"].PrefixPosition(), users => users
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = ImpersonationSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )
                    )
                );

            var isImpersonatingClaim = _httpContext.User.FindFirst(ClaimTypesExtended.IsImpersonating);
            if (_impersonationSettings.EndImpersonation && isImpersonatingClaim?.Value == "true")
            {
                builder
                    .Add(S["Security"], NavigationConstants.AdminMenuSecurityPosition, security => security
                        .AddClass("security").Id("security")
                        .Add(S["End Impersonation"], S["End Impersonation"].PrefixPosition(), users => users
                            .AddClass("endImpersonation").Id("endImpersonation")
                            .Action(nameof(ImpersonationController.EndImpersonatation), typeof(ImpersonationController).ControllerName(), "OrchardCoreContrib.Users")
                            .LocalNav()
                        )
                    );
            }

            return Task.CompletedTask;
        }
    }
}
