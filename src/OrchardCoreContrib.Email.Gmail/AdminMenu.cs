using Microsoft.Extensions.Localization;
using NavigationBuilder = OrchardCore.Navigation.NavigationBuilder;
using OrchardCoreContrib.Email.Gmail.Drivers;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.Email.Gmail
{
    /// <summary>
    /// Represents an admin menu for Gmaail mailing module.
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
                       .Add(S["Gmail"], S["Gmail"].PrefixPosition(), entry => entry
                       .AddClass("gmail").Id("gmail")
                          .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GmailSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageGmailSettings)
                          .LocalNav()
                        )
                    )
                );
        }
    }
}
