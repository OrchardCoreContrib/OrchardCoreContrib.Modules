using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Users;
using OrchardCoreContrib.Users.Drivers;

namespace OrchardCoreContrib.Users
{
    /// <summary>
    /// Represents an admin menu for the impersonation feature.
    /// </summary>
    [Feature("OrchardCore.Users.Impersonation")]
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer<AdminMenu>"/>.</param>
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        /// <inheritdoc/>
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Security"], security => security
                    .Add(S["Settings"], settings => settings
                        .Add(S["Impersonation"], S["Impersonation"].PrefixPosition(), password => password
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = ImpersonationSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }
}
