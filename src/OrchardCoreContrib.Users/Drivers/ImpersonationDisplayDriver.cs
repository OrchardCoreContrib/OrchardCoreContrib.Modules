using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Modules;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;

namespace OrchardCoreContrib.Users.Drivers;

[Feature("OrchardCoreContrib.Users.Impersonation")]
public class ImpersonationDisplayDriver : DisplayDriver<User>
{
    public override IDisplayResult Display(User user)
        => Initialize<SummaryAdminUserViewModel>("ImpersonationButton", model => model.User = user)
            .Location("SummaryAdmin", "Actions:2");
}
