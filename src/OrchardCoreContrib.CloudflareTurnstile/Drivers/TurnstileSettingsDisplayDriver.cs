using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.CloudflareTurnstile.Configuration;
using OrchardCoreContrib.CloudflareTurnstile.Settings;
using OrchardCoreContrib.CloudflareTurnstile.ViewModels;

namespace OrchardCoreContrib.CloudflareTurnstile.Drivers;

public class TurnstileSettingsDisplayDriver(
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService,
    IShellReleaseManager shellReleaseManager,
    IDataProtectionProvider dataProtectionProvider) : SiteDisplayDriver<TurnstileSettings>
{
    public const string GroupId = "Turnstile";

    protected override string SettingsGroupId => GroupId;

    public async override Task<IDisplayResult> EditAsync(ISite site, TurnstileSettings settings, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, TurnstilePermissions.ManageTurnstileSettings))
        {
            return null;
        }

        context.AddTenantReloadWarningWrapper();

        return Initialize<TurnstileSettingsViewModel>("TurnstileSettings_Edit", model =>
        {
            model.SiteKey = settings.SiteKey;
            model.SecretKey = settings.SecretKey;
            model.Theme = settings.Theme;
            model.Size = settings.Size;
        }).Location("Content:5")
        .OnGroup(GroupId);
    }

    public async override Task<IDisplayResult> UpdateAsync(ISite site, TurnstileSettings settings, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, TurnstilePermissions.ManageTurnstileSettings))
        {
            return null;
        }

        var model = new TurnstileSettingsViewModel();
        
        await context.Updater.TryUpdateModelAsync(model, Prefix);

        var protector = dataProtectionProvider.CreateProtector(TurnstileOptionsConfiguration.ProtectorName);

        if (!string.IsNullOrWhiteSpace(model.SiteKey))
        {
            if (settings.SiteKey != model.SiteKey)
            {
                settings.SiteKey = protector.Protect(model.SiteKey);
            }
        }

        if (!string.IsNullOrWhiteSpace(model.SecretKey))
        {
            if (settings.SecretKey != model.SecretKey)
            {
                settings.SecretKey = protector.Protect(model.SecretKey);
            }
        }

        settings.Theme = model.Theme;
        settings.Size = model.Size;

        if (context.Updater.ModelState.IsValid)
        {
            shellReleaseManager.RequestRelease();
        }

        return await EditAsync(site, settings, context);
    }
}

