using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.Gmail.Drivers;

namespace OrchardCoreContrib.Email.Gmail;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for Gmaail mailing module.
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
        { "groupId", GmailSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["Gmail"], S["Gmail"].PrefixPosition(), entry => entry
                   .AddClass("gmail").Id("gmail")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(GmailPermissions.ManageGmailSettings)
                      .LocalNav()
                    )
                )
            );
    }
}
