﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using NavigationBuilder = OrchardCore.Navigation.NavigationBuilder;
using OrchardCoreContrib.Navigation;
using OrchardCoreContrib.Users.Controllers;

namespace OrchardCoreContrib.Users
{
    /// <summary>
    /// Represents an admin menu for the impersonation feature.
    /// </summary>
    [Feature("OrchardCoreContrib.Users.Impersonation")]
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly HttpContext _httpContext;
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer<AdminMenu>"/>.</param>
        public AdminMenu(
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<AdminMenu> localizer)
        {
            _httpContext = httpContextAccessor.HttpContext;
            S = localizer;
        }

        /// <inheritdoc/>
        public override void BuildNavigation(NavigationBuilder builder)
        {
            var isImpersonatingClaim = _httpContext.User.FindFirst(ClaimTypesExtended.IsImpersonating);
            
            if (isImpersonatingClaim?.Value == "true")
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
        }
    }
}
