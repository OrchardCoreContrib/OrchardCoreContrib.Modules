using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.Yahoo.Services;

namespace OrchardCoreContrib.Email.Yahoo.Drivers;

/// <summary>
/// Represents a display driver for <see cref="YahooSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="YahooSettingsDisplayDriver"/>.
/// </remarks>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="shellHost">The <see cref="IShellHost"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
/// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
public class YahooSettingsDisplayDriver(
    IDataProtectionProvider dataProtectionProvider,
    IShellHost shellHost,
    ShellSettings shellSettings,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, YahooSettings>
{
    public const string GroupId = "yahoo";

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(ISite model, YahooSettings section, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, YahooPermissions.ManageYahooSettings))
        {
            return null;
        }

        var shapes = new List<IDisplayResult>
        {
            Initialize<YahooSettings>("YahooSettings_Edit", model =>
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
            shapes.Add(Dynamic("YahooSettings_TestButton").Location("Actions").OnGroup(GroupId));
        }

        return Combine(shapes);
    }

    /// <inheritdoc/>
    public override async Task<IDisplayResult> UpdateAsync(ISite model, YahooSettings section, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, YahooPermissions.ManageYahooSettings))
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
                var protector = dataProtectionProvider.CreateProtector(nameof(YahooSettingsConfiguration));
                section.Password = protector.Protect(section.Password);
            }

            await shellHost.ReleaseShellContextAsync(shellSettings);
        }

        return await EditAsync(model, section, context);
    }
}
