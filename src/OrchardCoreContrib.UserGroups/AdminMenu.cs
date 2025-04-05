using Microsoft.Extensions.Localization;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Navigation;
using OrchardCoreContrib.UserGroups.Controllers;

namespace OrchardCoreContrib.UserGroups;

using OrchardCoreContrib.Navigation;

public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder
            .Add(S["Security"], NavigationConstants.AdminMenuConfigurationPosition, builder => builder
                .Add(S["User Groups"], S["User Groups"].PrefixPosition(), builder => builder
                    .Id("usergroups")
                    .Action(nameof(AdminController.Index), typeof(AdminController).ControllerName(), "OrchardCoreContrib.UserGroups")
                    .LocalNav()));
    }
}
