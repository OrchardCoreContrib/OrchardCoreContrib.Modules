using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCoreContrib.Ban.Models;
using OrchardCoreContrib.Ban.ViewModels;
using System.Net;

namespace OrchardCoreContrib.Ban.Drivers;

public class BanSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor) : SiteDisplayDriver<BanSettings>
{
    private const char IPSeparator = ',';
    public const string GroupId = "ban";

    protected override string SettingsGroupId => GroupId;

    public async override Task<IDisplayResult> EditAsync(ISite site, BanSettings settings, BuildEditorContext context)
    {
        if (!await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, BanPermissions.ManageBanSettings))
        {
            return null;
        }

        return Initialize<BanSettingsViewModel>("BanSettings_Edit", model => 
            {
                model.BannedIPs = string.Join(IPSeparator, settings.BannedIPs);
                model.RedirectUrl = settings.RedirectUrl;
            })
            .Location("Content:5")
            .OnGroup(GroupId);
    }

    public async override Task<IDisplayResult> UpdateAsync(ISite site, BanSettings settings, UpdateEditorContext context)
    {
        if (!await authorizationService.AuthorizeAsync(httpContextAccessor.HttpContext.User, BanPermissions.ManageBanSettings))
        {
            return null;
        }

        var model = new BanSettingsViewModel();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        settings.BannedIPs = string.IsNullOrEmpty(model.BannedIPs)
            ? []
            : [.. model.BannedIPs
                .Split(IPSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Where(ip => IPAddress.TryParse(ip, out _))];

        if (string.IsNullOrEmpty(model.RedirectUrl))
        {
            return await EditAsync(site, settings, context);
        }

        var redirectUrl = model.RedirectUrl.Trim();
        if (RedirectHttpResult.IsLocalUrl(redirectUrl))
        {
            settings.RedirectUrl = redirectUrl;
        }
        else
        {
            // Avoid an external URL to prevent open redirect vulnerabilities
            context.Updater.ModelState.AddModelError(nameof(model.RedirectUrl), "Redirect URL must be a local URL.");
        }

        return await EditAsync(site, settings, context);
    }
}
