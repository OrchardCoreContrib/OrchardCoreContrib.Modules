using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Security.Services;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using OrchardCoreContrib.ContentPermissions.Models;
using OrchardCoreContrib.ContentPermissions.Services;
using OrchardCoreContrib.ContentPermissions.ViewModels;
using YesSql;

namespace OrchardCoreContrib.ContentPermissions.Drivers;

public class ContentPermissionsPartDisplayDriver(
    IContentPermissionsService contentPermissionsService,
    IHttpContextAccessor httpContextAccessor,
    IRoleService roleService,
    YesSql.ISession session) : ContentPartDisplayDriver<ContentPermissionsPart>
{
    public async override Task<IDisplayResult> DisplayAsync(ContentPermissionsPart part, BuildPartDisplayContext context)
    {
        if (context.DisplayType != "Detail" || await contentPermissionsService.AuthorizeAsync(part.ContentItem))
        {
            return null;
        }

        httpContextAccessor.HttpContext.Response.RedirectToAccessDeniedPage();

        return null;
    }

    public async override Task<IDisplayResult> EditAsync(ContentPermissionsPart part, BuildPartEditorContext context)
    {
        var settings = context.TypePartDefinition.GetSettings<ContentPermissionsPartSettings>();

        var roles = settings.EnableRoles
            ? await roleService.GetRoleNamesAsync()
            : [];

        var users = settings.EnableUsers
            ? await session.Query<User, UserIndex>().ListAsync()
            : [];

        return Initialize<ContentPermissionsPartViewModel>("ContentPermissionsPart_Edit", model =>
        {
            model.Roles = roles;
            model.SelectedRoles = part.Roles;
            model.Users = users.Select(u => u.UserName);
            model.SelectedUsers = part.Users;
        });
    }

    public async override Task<IDisplayResult> UpdateAsync(ContentPermissionsPart part, UpdatePartEditorContext context)
    {
        var model = new ContentPermissionsPartViewModel();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        part.Roles = model.SelectedRoles ?? [];
        part.Users = model.SelectedUsers ?? [];

        return Edit(part, context);
    }
}
