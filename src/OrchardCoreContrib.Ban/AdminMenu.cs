using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Ban.Drivers;

namespace OrchardCoreContrib.Ban;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for ban module.
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
        { "groupId", BanSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                    .Add(S["Ban"], S["Ban"].PrefixPosition(), ban => ban
                        .Id("ban")
                        .Action("Index", "Admin", _routeValues)
                        .Permission(BanPermissions.ManageBanSettings)
                        .LocalNav()
                    )
                )
            );
    }
}
