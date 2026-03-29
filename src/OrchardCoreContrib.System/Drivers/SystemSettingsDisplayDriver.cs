using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;

namespace OrchardCoreContrib.System.Drivers;

/// <summary>
/// Represents a display driver for <see cref="SystemSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="SystemSettingsDisplayDriver"/>.
/// </remarks>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
/// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
public class SystemSettingsDisplayDriver(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, SystemSettings>
{
    internal const string GroupId = "system";

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(ISite model, SystemSettings section, BuildEditorContext context)
    {
        if (!await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext?.User, SystemPermissions.ManageSystemSettings))
        {
            return null;
        }

        return Initialize<SystemSettings>("SystemSettings_Edit", model => model.AllowMaintenanceMode = section.AllowMaintenanceMode)
            .Location("Content:5")
            .OnGroup(GroupId);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(ISite model, SystemSettings section, UpdateEditorContext context)
    {
        if (!await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext?.User, SystemPermissions.ManageSystemSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            await context.Updater.TryUpdateModelAsync(section, Prefix);
        }

        return await EditAsync(model, section, context);
    }
}
