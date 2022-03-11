using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCore.DataLocalization
{
    /// <summary>
    /// Represents a localization menu in the admin site.
    /// </summary>
    public class AdminMenu : INavigationProvider
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
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder
                    .Add(S["Configuration"], NavigationConstants.AdminMenuConfigurationPosition, localization => localization
                        .Add(S["Settings"], settings => settings
                            .Add(S["Localization"], localization => localization
                                .AddClass("localization").Id("localization")
                                .Add(S["Data Resources"], S["Data Resources"].PrefixPosition(), data => data
                                    .AddClass("data-resources").Id("data-resources")
                                    .Add(S["Content Types"], S["Content Types"].PrefixPosition(), type => type
                                        .Action("ManageContentTypeResources", "Admin", new { area = "OrchardCoreContrib.DataLocalization"})
                                        .LocalNav()
                                    )
                                )
                            )
                        )
                    );
            }

            return Task.CompletedTask;
        }
    }
}
