using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Navigation;

namespace OrchardCore.DataLocalization
{
    using OrchardCoreContrib.Navigation;

    /// <summary>
    /// Represents a localization menu in the admin site.
    /// </summary>
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Creates a new instance of the <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        ///<inheritdocs />
        public override void BuildNavigation(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], NavigationConstants.AdminMenuConfigurationPosition, localization => localization
                    .Add(S["Settings"], settings => settings
                        .Add(S["Localization"], localization => localization
                            .AddClass("localization").Id("localization")
                            .Add(S["Data Resources"], S["Data Resources"].PrefixPosition(), data => data
                                .AddClass("data-resources").Id("data-resources")
                                .Add(S["Content Types"], S["Content Types"].PrefixPosition(), type => type
                                    .Action("ManageContentTypeResources", "Admin", new { area = "OrchardCoreContrib.DataLocalization" })
                                    .LocalNav()
                                )
                                .Add(S["Content Fields"], S["Content Fields"].PrefixPosition(), type => type
                                    .Action("ManageContentFieldResources", "Admin", new { area = "OrchardCoreContrib.DataLocalization" })
                                    .LocalNav()
                                )
                            )
                        )
                    )
                );
        }
    }
}
