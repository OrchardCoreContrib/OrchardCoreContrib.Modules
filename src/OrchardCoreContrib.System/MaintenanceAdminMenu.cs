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
        => builder.Add(S["System"], "100", system => system.AddClass("system").Id("system")
            .Add(S["Maintenance"], S["Maintenance"].PrefixPosition(), maintenance => maintenance
                .Action("Index", "Admin", _routeValues)
                .Permission(SystemPermissions.ManageSystemSettings)
                .LocalNav())
        );
}
