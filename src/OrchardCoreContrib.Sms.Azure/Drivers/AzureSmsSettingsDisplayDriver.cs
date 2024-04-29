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

    private readonly IPhoneNumberValidator _phoneNumberValidator = phoneNumberValidator;
    private readonly IDataProtectionProvider _dataProtectionProvider = dataProtectionProvider;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly IShellHost _shellHost = shellHost;
    private readonly ShellSettings _shellSettings = shellSettings;

    public override async Task<IDisplayResult> EditAsync(AzureSmsSettings settings, BuildEditorContext context)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (!await _authorizationService.AuthorizeAsync(user, AzureSmsPermissions.ManageSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
            {
                Initialize<AzureSmsSettings>("AzureSmsSettings_Edit", model =>
                {
                    model.ConnectionString = settings.ConnectionString;
                    model.SenderPhoneNumber = settings.SenderPhoneNumber;
                }).Location("Content:5").OnGroup(GroupId)
            };

        if (settings.SenderPhoneNumber is not null)
        {
            shapes.Add(Dynamic("AzureSmsSettings_TestButton").Location("Actions").OnGroup(GroupId));
        }

        return Combine(shapes);
    }

    public override async Task<IDisplayResult> UpdateAsync(AzureSmsSettings settings, BuildEditorContext context)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (!await _authorizationService.AuthorizeAsync(user, AzureSmsPermissions.ManageSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            var previousConnectionString = settings.ConnectionString;
           
            await context.Updater.TryUpdateModelAsync(settings, Prefix);

            if (!_phoneNumberValidator.IsValid(settings.SenderPhoneNumber))
            {
                context.Updater.ModelState.AddModelError(nameof(AzureSmsSettings.SenderPhoneNumber), H["Invalid Phone Number."].Value);

                return await EditAsync(settings, context);
            }

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                settings.ConnectionString = previousConnectionString;
            }
            else
            {
                var protector = _dataProtectionProvider.CreateProtector(nameof(AzureSmsSettings));
                settings.ConnectionString = protector.Protect(settings.ConnectionString);
            }

            await _shellHost.ReleaseShellContextAsync(_shellSettings);
        }

        return await EditAsync(settings, context);
    }
}
