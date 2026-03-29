using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.DataLocalization;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents a localization menu in the admin site.
/// </summary>
/// <remarks>
/// Creates a new instance of the <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="S">The <see cref="IStringLocalizer"/>.</param>
public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCoreContrib.DataLocalization"  }
    };

    ///<inheritdocs />
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], NavigationConstants.AdminMenuConfigurationPosition, localization => localization
                .Add(S["Settings"], settings => settings
                    .Add(S["Localization"], localization => localization
                        .AddClass("localization").Id("localization")
                        .Add(S["Data Resources"], S["Data Resources"].PrefixPosition(), data => data
                            .AddClass("data-resources").Id("data-resources")
                            .Add(S["Content Types"], S["Content Types"].PrefixPosition(), type => type
                                .Action("ManageContentTypeResources", "Admin", _routeValues)
                                .LocalNav()
                            )
                            .Add(S["Content Fields"], S["Content Fields"].PrefixPosition(), type => type
                                .Action("ManageContentFieldResources", "Admin", _routeValues)
                                .LocalNav()
                            )
                        )
                    )
                )
            );
    }
}
