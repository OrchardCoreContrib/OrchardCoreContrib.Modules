using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Settings;
using OrchardCoreContrib.CloudflareTurnstile.Settings;

namespace OrchardCoreContrib.CloudflareTurnstile.Configuration;

public class TurnstileOptionsConfiguration(
    ISiteService siteService,
    IDataProtectionProvider dataProtectionProvider,
    ILogger<TurnstileOptionsConfiguration> logger) : IConfigureOptions<TurnstileOptions>
{
    public const string ProtectorName = "TurnstileSettingsConfiguration";

    public void Configure(TurnstileOptions options)
    {
        var settings = siteService.GetSettings<TurnstileSettings>();

        var protector = dataProtectionProvider.CreateProtector(ProtectorName);

        if (!string.IsNullOrWhiteSpace(settings.SiteKey))
        {
            try
            {
                options.SiteKey = protector.Unprotect(settings.SiteKey);
            }
            catch
            {
                logger.LogError("The site key could not be decrypted. It may have been encrypted using a different key.");
            }
        }

        if (!string.IsNullOrWhiteSpace(settings.SecretKey))
        {
            try
            {
                options.SecretKey = protector.Unprotect(settings.SecretKey);
            }
            catch
            {
                logger.LogError("The secret key could not be decrypted. It may have been encrypted using a different key.");
            }
        }

        options.Theme = settings.Theme;
        options.Size = settings.Size;
    }
}
