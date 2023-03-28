using Microsoft.Extensions.Localization;
using NavigationBuilder = OrchardCore.Navigation.NavigationBuilder;
using OrchardCoreContrib.Email.Yahoo.Drivers;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.Email.Yahoo
{
    /// <summary>
    /// Represents an admin menu for Yahoo mailing module.
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
                       .Add(S["Yahoo"], S["Yahoo"].PrefixPosition(), entry => entry
                       .AddClass("yahoo").Id("yahoo")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = YahooSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageYahooSettings)
                          .LocalNav()
                       )
                    )
                );
        }
    }
}
