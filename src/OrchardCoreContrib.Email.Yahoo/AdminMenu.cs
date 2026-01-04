using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.Yahoo.Drivers;

namespace OrchardCoreContrib.Email.Yahoo;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for Yahoo mailing module.
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
        { "groupId", YahooSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["Yahoo"], S["Yahoo"].PrefixPosition(), entry => entry
                   .AddClass("yahoo").Id("yahoo")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(YahooPermissions.ManageYahooSettings)
                      .LocalNav()
                   )
                )
            );
    }
}
