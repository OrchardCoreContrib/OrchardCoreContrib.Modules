using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.SendGrid.Services;

namespace OrchardCoreContrib.Email.SendGrid.Drivers;

/// <summary>
/// Represents a display driver for <see cref="SendGridSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="SendGridSettingsDisplayDriver"/>.
/// </remarks>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="shellHost">The <see cref="IShellHost"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
/// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
public class SendGridSettingsDisplayDriver(
    IDataProtectionProvider dataProtectionProvider,
    IShellHost shellHost,
    ShellSettings shellSettings,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, SendGridSettings>
{
    public const string GroupId = "sendgrid";

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(ISite model, SendGridSettings section, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, SendGridPermissions.ManageSendGridSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
        {
            Initialize<SendGridSettings>("SendGridSettings_Edit", model =>
            {
                model.DefaultSender = section.DefaultSender;
                model.ApiKey = section.ApiKey;
            }).Location("Content:5").OnGroup(GroupId)
        };

        if (section?.DefaultSender != null)
        {
            shapes.Add(Dynamic("SendGridSettings_TestButton").Location("Actions").OnGroup(GroupId));
        }

        return Combine(shapes);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(ISite model, SendGridSettings section, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, SendGridPermissions.ManageSendGridSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            var previousApiKey = section.ApiKey;
            await context.Updater.TryUpdateModelAsync(section, Prefix);

            if (string.IsNullOrWhiteSpace(section.ApiKey))
            {
                section.ApiKey = previousApiKey;
            }
            else
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(SendGridSettingsConfiguration));
                section.ApiKey = protector.Protect(section.ApiKey);
            }

            await shellHost.ReleaseShellContextAsync(shellSettings);
        }

        return await EditAsync(model, section, context);
    }
}
