using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.Hotmail.Services;

/// <summary>
/// Represents a configuration for <see cref="HotmailSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="HotmailSettingsConfiguration"/>.
/// </remarks>
/// <param name="site">The <see cref="ISiteService"/>.</param>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="logger">The <see cref="ILogger<HotmailSettingsConfiguration>"/>.</param>
public class HotmailSettingsConfiguration(
    ISiteService site,
    IDataProtectionProvider dataProtectionProvider,
    ILogger<HotmailSettingsConfiguration> logger) : IConfigureOptions<HotmailSettings>
{

    /// <inheritdoc/>
    public void Configure(HotmailSettings hotmailSettings)
    {
        var settings = site.GetSiteSettingsAsync()
            .GetAwaiter().GetResult()
            .As<HotmailSettings>();

        hotmailSettings.DefaultSender = settings.DefaultSender;
        hotmailSettings.Host = settings.Host;
        hotmailSettings.Port = settings.Port;
        hotmailSettings.EncryptionMethod = settings.EncryptionMethod;
        hotmailSettings.AutoSelectEncryption = settings.AutoSelectEncryption;
        hotmailSettings.RequireCredentials = settings.RequireCredentials;
        hotmailSettings.UseDefaultCredentials = settings.UseDefaultCredentials;
        hotmailSettings.UserName = settings.UserName;

        if (!string.IsNullOrWhiteSpace(settings.Password))
        {
            try
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(HotmailSettingsConfiguration));
                hotmailSettings.Password = protector.Unprotect(settings.Password);
            }
            catch
            {
                logger.LogError("The Hotmail password could not be decrypted. It may have been encrypted using a different key.");
            }
        }
    }
}
