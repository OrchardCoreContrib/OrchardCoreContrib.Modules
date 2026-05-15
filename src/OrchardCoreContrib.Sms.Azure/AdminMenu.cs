using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Sms.Azure.Drivers;

namespace OrchardCoreContrib.Sms.Azure;

using OrchardCoreContrib.Navigation;

public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", AzureSmsSettingsDisplayDriver.GroupId },
    };

    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                   .Add(S["Azure SMS"], S["Azure SMS"].PrefixPosition(), sms => sms
                   .AddClass("azure-sms").Id("azuresms")
                      .Action("Index", "Admin", _routeValues)
                      .Permission(AzureSmsPermissions.ManageAzureSmsSettings)
                      .LocalNav()
                    )
                )
            );
    }
}
