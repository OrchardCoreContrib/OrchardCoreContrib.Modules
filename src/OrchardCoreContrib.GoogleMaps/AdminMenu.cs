using Microsoft.Extensions.Localization;
using OrchardCoreContrib.GoogleMaps.Drivers;
using OrchardCore.Navigation;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.GoogleMaps
{
    /// <summary>
    /// Represents an admin menu for GoogleMaps module.
    /// </summary>
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="stringLocalizer"></param>
        public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
        {
            S = stringLocalizer;
        }

        /// <inheritdoc/>
        public override void BuildNavigation(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                       .Add(S["Google Maps"], S["Google Maps"].PrefixPosition(), entry => entry
                       .AddClass("googlemaps").Id("googlemaps")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GoogleMapsSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageGoogleMapsSettings)
                          .LocalNav()
                        )
                    )
                );
        }
    }
}
