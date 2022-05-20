using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.ContentLocalization
{
    [Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

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

            return Task.CompletedTask;
        }
    }
}
