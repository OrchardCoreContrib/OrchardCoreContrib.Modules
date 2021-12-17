using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.SendGrid.Services
{
    /// <summary>
    /// Represents a configuration for <see cref="SendGridSettings"/>.
    /// </summary>
    public class SendGridSettingsConfiguration : IConfigureOptions<SendGridSettings>
    {
        private readonly ISiteService _site;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="SendGridSettingsConfiguration"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISiteService"/>.</param>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="logger">The <see cref="ILogger<GmailSettingsConfiguration>"/>.</param>
        public SendGridSettingsConfiguration(
            ISiteService site,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<SendGridSettingsConfiguration> logger)
        {
            _site = site;
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Configure(SendGridSettings sendGridSettings)
        {
            var settings = _site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<SendGridSettings>();

            sendGridSettings.DefaultSender = settings.DefaultSender;

            if (!string.IsNullOrWhiteSpace(settings.ApiKey))
            {
                try
                {
                    var protector = _dataProtectionProvider.CreateProtector(nameof(SendGridSettingsConfiguration));
                    sendGridSettings.ApiKey = protector.Unprotect(settings.ApiKey);
                }
                catch
                {
                    _logger.LogError("The SendGrid API key could not be decrypted. It may have been encrypted using a different key.");
                }
            }
        }
    }
}
