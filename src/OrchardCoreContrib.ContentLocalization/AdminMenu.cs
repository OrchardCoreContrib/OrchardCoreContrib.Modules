using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.ContentLocalization;

using OrchardCoreContrib.Navigation;

[Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    private static readonly RouteValueDictionary _routeValues = new()
    {
        { "area", "OrchardCoreContrib.ContentLocalization" }
    };

    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Configuration"], config => config
                .Add(S["Settings"], settings => settings
                    .Add(S["Localization"], localization => localization
                        .AddClass("localization").Id("localization")
                        .Add(S["Localization Matrix"], S["Localization Matrix"].PrefixPosition(), localizationMatrix => localizationMatrix
                            .Action("LocalizationMatrix", "Admin", _routeValues)
                            .LocalNav()
                         )
                    )
                )
            );
    }
}
