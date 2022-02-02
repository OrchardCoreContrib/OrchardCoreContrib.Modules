using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Gdpr.Drivers;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Gdpr
{
    /// <summary>
    /// Represents an admin menu for GDPR module.
    /// </summary>
    public class AdminMenu : INavigationProvider
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
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                       .Add(S["GDPR"], S["GDPR"].PrefixPosition(), entry => entry
                       .AddClass("gdpr").Id("gdpr")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GdprSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageGdprSettings)
                          .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
