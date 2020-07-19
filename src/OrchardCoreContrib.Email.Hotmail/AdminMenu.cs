using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCoreContrib.Email.Hotmail.Drivers;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.Email.Hotmail
{
    /// <summary>
    /// Represents an admin menu for Hotmail mailing module.
    /// </summary>
    public class AdminMenu : INavigationProvider
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
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                       .Add(S["Hotmail"], S["Hotmail"].PrefixPosition(), entry => entry
                       .AddClass("hotmail").Id("hotmail")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = HotmailSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageHotmailSettings)
                          .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
