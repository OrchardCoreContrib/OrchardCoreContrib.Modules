using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Gdpr.Drivers;

/// <summary>
/// Represents a display driver for <see cref="GdprSettings"/>.
/// </summary>
public class GdprSettingsDisplayDriver(
    IShellHost shellHost,
    ShellSettings shellSettings,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, GdprSettings>
{
    public const string GroupId = "gdpr";

    /// <inheritdoc/>

    public override async Task<IDisplayResult> EditAsync(ISite model, GdprSettings section, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GdprPermissions.ManageGdprSettings))
        {
            return null;
        }

        return Initialize<GdprSettings>("GdprSettings_Edit", model =>
        {
            model.Summary = section.Summary;
            model.Detail = section.Detail;
        }).Location("Content:5")
            .OnGroup(GroupId);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(ISite model, GdprSettings section, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GdprPermissions.ManageGdprSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            await context.Updater.TryUpdateModelAsync(section, Prefix);

            await shellHost.ReleaseShellContextAsync(shellSettings);
        }

        return await EditAsync(model, section, context);
    }
}
