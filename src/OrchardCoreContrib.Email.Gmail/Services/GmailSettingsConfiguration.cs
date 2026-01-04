using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.Gmail.Services;

/// <summary>
/// Represents a configuration for <see cref="GmailSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="GmailSettingsConfiguration"/>.
/// </remarks>
/// <param name="site">The <see cref="ISiteService"/>.</param>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="logger">The <see cref="ILogger<GmailSettingsConfiguration>"/>.</param>
public class GmailSettingsConfiguration(
    ISiteService site,
    IDataProtectionProvider dataProtectionProvider,
    ILogger<GmailSettingsConfiguration> logger) : IConfigureOptions<GmailSettings>
{
    /// <inheritdoc/>
    public void Configure(GmailSettings gmailSettings)
    {
        var settings = site.GetSiteSettingsAsync()
            .GetAwaiter()
            .GetResult()
            .As<GmailSettings>();

        gmailSettings.DefaultSender = settings.DefaultSender;
        gmailSettings.Host = settings.Host;
        gmailSettings.Port = settings.Port;
        gmailSettings.EncryptionMethod = settings.EncryptionMethod;
        gmailSettings.AutoSelectEncryption = settings.AutoSelectEncryption;
        gmailSettings.RequireCredentials = settings.RequireCredentials;
        gmailSettings.UseDefaultCredentials = settings.UseDefaultCredentials;
        gmailSettings.UserName = settings.UserName;

        if (!string.IsNullOrWhiteSpace(settings.Password))
        {
            try
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(GmailSettingsConfiguration));

                gmailSettings.Password = protector.Unprotect(settings.Password);
            }
            catch
            {
                logger.LogError("The Gmail password could not be decrypted. It may have been encrypted using a different key.");
            }
        }
    }
}
