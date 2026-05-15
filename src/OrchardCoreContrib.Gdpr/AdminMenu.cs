using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Gdpr.Drivers;

namespace OrchardCoreContrib.Gdpr;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for GDPR module.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="stringLocalizer">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", GdprSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["GDPR"], S["GDPR"].PrefixPosition(), entry => entry
                   .AddClass("gdpr").Id("gdpr")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(GdprPermissions.ManageGdprSettings)
                      .LocalNav()
            )));
    }
}
