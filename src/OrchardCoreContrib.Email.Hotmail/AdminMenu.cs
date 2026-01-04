using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.Hotmail.Drivers;

namespace OrchardCoreContrib.Email.Hotmail;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for Hotmail mailing module.
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
        { "groupId", HotmailSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["Hotmail"], S["Hotmail"].PrefixPosition(), entry => entry
                   .AddClass("hotmail").Id("hotmail")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(HotmailPermissions.ManageHotmailSettings)
                      .LocalNav()
                    )
                )
            );
    }
}
