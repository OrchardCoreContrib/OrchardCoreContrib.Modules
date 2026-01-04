using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.SendGrid.Services;

/// <summary>
/// Represents a configuration for <see cref="SendGridSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="SendGridSettingsConfiguration"/>.
/// </remarks>
/// <param name="site">The <see cref="ISiteService"/>.</param>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="logger">The <see cref="ILogger<GmailSettingsConfiguration>"/>.</param>
public class SendGridSettingsConfiguration(
    ISiteService site,
    IDataProtectionProvider dataProtectionProvider,
    ILogger<SendGridSettingsConfiguration> logger) : IConfigureOptions<SendGridSettings>
{
    /// <inheritdoc/>
    public void Configure(SendGridSettings sendGridSettings)
    {
        var settings = site.GetSiteSettingsAsync()
            .GetAwaiter().GetResult()
            .As<SendGridSettings>();

        sendGridSettings.DefaultSender = settings.DefaultSender;

        if (!string.IsNullOrWhiteSpace(settings.ApiKey))
        {
            try
            {
                var protector = dataProtectionProvider.CreateProtector(nameof(SendGridSettingsConfiguration));
                sendGridSettings.ApiKey = protector.Unprotect(settings.ApiKey);
            }
            catch
            {
                logger.LogError("The SendGrid API key could not be decrypted. It may have been encrypted using a different key.");
            }
        }
    }
}
