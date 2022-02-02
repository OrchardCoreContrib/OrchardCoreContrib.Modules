using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.Yahoo.Drivers;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Yahoo
{
    /// <summary>
    /// Represents an admin menu for Yahoo mailing module.
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
                       .Add(S["Yahoo"], S["Yahoo"].PrefixPosition(), entry => entry
                       .AddClass("yahoo").Id("yahoo")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = YahooSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageYahooSettings)
                          .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
