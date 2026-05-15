using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCoreContrib.Users.Controllers;

namespace OrchardCoreContrib.Users;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for the impersonation feature.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="S">The <see cref="IStringLocalizer<AdminMenu>"/>.</param>
[Feature("OrchardCoreContrib.Users.Impersonation")]
public class AdminMenu(
    IHttpContextAccessor httpContextAccessor,
    IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext;

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
