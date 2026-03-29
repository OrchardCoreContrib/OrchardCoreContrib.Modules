using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.CloudflareTurnstile.Drivers;

namespace OrchardCoreContrib.CloudflareTurnstile;

using OrchardCoreContrib.Navigation;

public sealed class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCore.Settings" },
        { "groupId", TurnstileSettingsDisplayDriver.GroupId },
    };

    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], settings => settings
                    .Add(S["Security"], S["Security"].PrefixPosition(), security => security
                        .Add(S["Turnstile"], S["Turnstile"].PrefixPosition(), turnstile => turnstile
                            .Id("turnstile")
                            .Permission(TurnstilePermissions.ManageTurnstileSettings)
                            .Action("Index", "Admin", _routeValues)
                            .LocalNav()
                        )
                    )
                )
            );
    }
}
