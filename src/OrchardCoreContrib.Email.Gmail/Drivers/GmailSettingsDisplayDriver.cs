using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.Gmail.Services;

namespace OrchardCoreContrib.Email.Gmail.Drivers;

/// <summary>
/// Represents a display driver for <see cref="GmailSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="GmailSettingsDisplayDriver"/>.
/// </remarks>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="shellHost">The <see cref="IShellHost"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
/// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
public class GmailSettingsDisplayDriver(
    IDataProtectionProvider dataProtectionProvider,
    IShellHost shellHost,
    ShellSettings shellSettings,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, GmailSettings>
{
    public const string GroupId = "gmail";

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(ISite model, GmailSettings section, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GmailPermissions.ManageGmailSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
        {
            Initialize<GmailSettings>("GmailSettings_Edit", model =>
            {
                model.DefaultSender = section.DefaultSender;
                model.DeliveryMethod = section.DeliveryMethod;
                model.PickupDirectoryLocation = section.PickupDirectoryLocation;
                model.Host = section.Host;
                model.Port = section.Port;
                model.EncryptionMethod = section.EncryptionMethod;
                model.AutoSelectEncryption = section.AutoSelectEncryption;
                model.RequireCredentials = section.RequireCredentials;
                model.UseDefaultCredentials = section.UseDefaultCredentials;
                model.UserName = section.UserName;
                model.Password = section.Password;
            }).Location("Content:5").OnGroup(GroupId)
        };

        if (section?.DefaultSender != null)
        {
            shapes.Add(Dynamic("GmailSettings_TestButton")
                .Location("Actions")
                .OnGroup(GroupId));
        }

        return Combine(shapes);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(ISite model, GmailSettings section, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GmailPermissions.ManageGmailSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            var previousPassword = section.Password;
            await context.Updater.TryUpdateModelAsync(section, Prefix);

            if (string.IsNullOrWhiteSpace(section.Password))
            {
                section.Password = previousPassword;
            }
            else
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(GmailSettingsConfiguration));
                section.Password = protector.Protect(section.Password);
            }

            await shellHost.ReleaseShellContextAsync(shellSettings);
        }

        return await EditAsync(model, section, context);
    }
}
