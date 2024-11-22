using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Gdpr.Drivers;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Gdpr
{
    using OrchardCoreContrib.Navigation;

    /// <summary>
    /// Represents an admin menu for GDPR module.
    /// </summary>
    public class AdminMenu : AdminNavigationProvider
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
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
                       .Add(S["GDPR"], S["GDPR"].PrefixPosition(), entry => entry
                       .AddClass("gdpr").Id("gdpr")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GdprSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageGdprSettings)
                          .LocalNav()
                )));
        }
    }
}
