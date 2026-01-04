using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.SendGrid.Drivers;

namespace OrchardCoreContrib.Email.SendGrid;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for SendGrid mailing module.
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
        { "groupId", SendGridSettingsDisplayDriver.GroupId },
    };

    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                    .Add(S["SendGrid"], S["SendGrid"].PrefixPosition(), entry => entry
                        .AddClass("sendgrid").Id("sendgrid")
                        .Action("Index", "Admin", _routeValues)
                        .Permission(SendGridPermissions.ManageSendGridSettings)
                        .LocalNav()
                    )
                )
            );
    }
}
