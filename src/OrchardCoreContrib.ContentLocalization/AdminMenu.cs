using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.ContentLocalization
{
    using OrchardCoreContrib.Navigation;

    [Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public override void BuildNavigation(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], c => c
                    .Add(S["Settings"], s => s
                        .Add(S["Localization"], l => l
                            .AddClass("localization").Id("localization")
                            .Add(S["Localization Matrix"], S["Localization Matrix"].PrefixPosition(), lm => lm
                                .Action("LocalizationMatrix", "Admin", new { area = "OrchardCoreContrib.ContentLocalization" })
                                .LocalNav()
                             )
                        )
                    )
                );
        }
    }
}
