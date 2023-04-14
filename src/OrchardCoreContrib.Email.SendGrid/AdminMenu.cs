using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Email.SendGrid.Drivers;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.Email.SendGrid
{
    /// <summary>
    /// Represents an admin menu for SendGrid mailing module.
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
                        .Add(S["SendGrid"], S["SendGrid"].PrefixPosition(), entry => entry
                            .AddClass("sendgrid").Id("sendgrid")
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = SendGridSettingsDisplayDriver.GroupId })
                            .Permission(Permissions.ManageSendGridSettings)
                            .LocalNav()
                        )
                    )
                );
        }
    }
}
