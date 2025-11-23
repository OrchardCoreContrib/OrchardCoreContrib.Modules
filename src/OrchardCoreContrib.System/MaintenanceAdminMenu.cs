using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.System.Drivers;

namespace OrchardCoreContrib.System;

using Microsoft.AspNetCore.Routing;
using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for maintenance feature.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="MaintenanceAdminMenu"/>.
/// </remarks>
/// <param name="S"></param>
public class MaintenanceAdminMenu(IStringLocalizer<MaintenanceAdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", SystemSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                    .Add(S["System"], S["System"].PrefixPosition(), system => system
                        .AddClass("system").Id("system")
                        .Add(S["Maintenance"], S["Maintenance"], maintenance => maintenance
                            .Action("Index", "Admin", _routeValues)
                            .Permission(SystemPermissions.ManageSystemSettings)
                            .LocalNav()
                       )
                   )
                )
            );
    }
}
