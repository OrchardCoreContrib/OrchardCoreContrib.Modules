using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.GoogleMaps.Drivers;

namespace OrchardCoreContrib.GoogleMaps;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for GoogleMaps module.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="stringLocalizer"></param>
public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", GoogleMapsSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["Google Maps"], S["Google Maps"].PrefixPosition(), entry => entry
                   .AddClass("googlemaps").Id("googlemaps")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(GoogleMapsPermissions.ManageGoogleMapsSettings)
                      .LocalNav()
                    )
                )
            );
    }
}
