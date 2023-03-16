using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;

namespace OrchardCoreContrib.System.Drivers;

/// <summary>
/// Represents a display driver for <see cref="SystemSettings"/>.
/// </summary>
public class SystemSettingsDisplayDriver : SectionDisplayDriver<ISite, SystemSettings>
{
    public const string GroupId = "system";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// Initializes a new instance of <see cref="SystemSettingsDisplayDriver"/>.
    /// </summary>
    /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
    /// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
    public SystemSettingsDisplayDriver(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(SystemSettings section, BuildEditorContext context)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSystemSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
        {
            Initialize<SystemSettings>("SystemSettings_Edit", model =>
            {
                model.AllowMaintenanceMode = section.AllowMaintenanceMode;
            }).Location("Content:5").OnGroup(GroupId)
        };

        return Combine(shapes);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(SystemSettings section, BuildEditorContext context)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSystemSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            await context.Updater.TryUpdateModelAsync(section, Prefix);
        }

        return await EditAsync(section, context);
    }
}
