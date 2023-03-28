using Microsoft.Extensions.Localization;
using NavigationBuilder = OrchardCore.Navigation.NavigationBuilder;
using OrchardCoreContrib.Navigation;
using OrchardCoreContrib.System.Drivers;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents an admin menu for maintenance feature.
/// </summary>
public class MaintenanceAdminMenu : AdminNavigationProvider
{
    private readonly IStringLocalizer S;

    /// <summary>
    /// Initializes a new instance of <see cref="MaintenanceAdminMenu"/>.
    /// </summary>
    /// <param name="stringLocalizer"></param>
    public MaintenanceAdminMenu(IStringLocalizer<MaintenanceAdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                    .Add(S["System"], S["System"].PrefixPosition(), system => system
                        .AddClass("system").Id("system")
                        .Add(S["Maintenance"], S["Maintenance"], maintenance => maintenance
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = SystemSettingsDisplayDriver.GroupId })
                            .Permission(Permissions.ManageSystemSettings)
                            .LocalNav()
                       )
                   )
                )
            );
    }
}
