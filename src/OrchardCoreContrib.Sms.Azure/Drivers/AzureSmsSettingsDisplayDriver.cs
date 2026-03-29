using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.Validation;

namespace OrchardCoreContrib.Sms.Azure.Drivers;

public class AzureSmsSettingsDisplayDriver(
    IPhoneNumberValidator phoneNumberValidator,
    IDataProtectionProvider dataProtectionProvider,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService,
    IShellHost shellHost,
    ShellSettings shellSettings,
    IHtmlLocalizer<AzureSmsSettingsDisplayDriver> H) : SectionDisplayDriver<ISite, AzureSmsSettings>
{
    public const string GroupId = "azure-sms";

    public override async Task<IDisplayResult> EditAsync(ISite model, AzureSmsSettings settings, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, AzureSmsPermissions.ManageAzureSmsSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
        {
            Initialize<AzureSmsSettings>("AzureSmsSettings_Edit", model =>
            {
                model.ConnectionString = settings.ConnectionString;
                model.SenderPhoneNumber = settings.SenderPhoneNumber;
            }).Location("Content:5")
            .OnGroup(GroupId)
        };

        if (settings.SenderPhoneNumber is not null)
        {
            shapes.Add(Dynamic("AzureSmsSettings_TestButton")
                .Location("Actions")
                .OnGroup(GroupId));
        }

        return Combine(shapes);
    }

    public override async Task<IDisplayResult> UpdateAsync(ISite model, AzureSmsSettings settings, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, AzureSmsPermissions.ManageAzureSmsSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            var previousConnectionString = settings.ConnectionString;
           
            await context.Updater.TryUpdateModelAsync(settings, Prefix);

            if (!phoneNumberValidator.IsValid(settings.SenderPhoneNumber))
            {
                context.Updater.ModelState.AddModelError(nameof(AzureSmsSettings.SenderPhoneNumber), H["Invalid Phone Number."].Value);

                return await EditAsync(model, settings, context);
            }

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                settings.ConnectionString = previousConnectionString;
            }
            else
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(AzureSmsSettings));
                settings.ConnectionString = protector.Protect(settings.ConnectionString);
            }

            await shellHost.ReleaseShellContextAsync(shellSettings);
        }

        return await EditAsync(model, settings, context);
    }
}
