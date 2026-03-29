using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using OrchardCoreContrib.UserGroups.Services;
using OrchardCoreContrib.UserGroups.ViewModels;

namespace OrchardCoreContrib.UserGroups.Drivers;

public class UserGroupsDisplayDriver(UserGroupsManager userGroupsManager) : DisplayDriver<User>
{
    public override IDisplayResult Display(User user, BuildDisplayContext context)
        => Initialize<UserGroupsViewModel>("UserGroups", model => model.UserGroups = user.GetUserGroups())
            .Location("DetailAdmin", "Content:10");

    public async override Task<IDisplayResult> EditAsync(User user, BuildEditorContext context)
    {
        var userGroupNames = await userGroupsManager.GetUserGroupNamesAsync();

        return Initialize<UserGroupsEditViewModel>("UserGroups_Edit", model =>
        {
            model.UserGroups = userGroupNames;
            model.SelectedUserGroups = user.GetUserGroups();
        }).Location("Content:10");
    }

    public async override Task<IDisplayResult> UpdateAsync(User user, UpdateEditorContext context)
    {
        var model = new UserGroupsEditViewModel();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        user.SetUserGroups(model.SelectedUserGroups);

        return Edit(user, context);
    }
}
