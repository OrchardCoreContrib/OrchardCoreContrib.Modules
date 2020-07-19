using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.Yahoo.Services
{
    /// <summary>
    /// Represents a configuration for <see cref="YahooSettings"/>.
    /// </summary>
    public class YahooSettingsConfiguration : IConfigureOptions<YahooSettings>
    {
        private readonly ISiteService _site;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="YahooSettingsConfiguration"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISiteService"/>.</param>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="logger">The <see cref="ILogger<YahooSettingsConfiguration>"/>.</param>
        public YahooSettingsConfiguration(
            ISiteService site,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<YahooSettingsConfiguration> logger)
        {
            _site = site;
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Configure(YahooSettings yahooSettings)
        {
            var settings = _site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<YahooSettings>();

            yahooSettings.DefaultSender = settings.DefaultSender;
            yahooSettings.Host = settings.Host;
            yahooSettings.Port = settings.Port;
            yahooSettings.EncryptionMethod = settings.EncryptionMethod;
            yahooSettings.AutoSelectEncryption = settings.AutoSelectEncryption;
            yahooSettings.RequireCredentials = settings.RequireCredentials;
            yahooSettings.UseDefaultCredentials = settings.UseDefaultCredentials;
            yahooSettings.UserName = settings.UserName;

            if (!string.IsNullOrWhiteSpace(settings.Password))
            {
                try
                {
                    var protector = _dataProtectionProvider.CreateProtector(nameof(YahooSettingsConfiguration));
                    yahooSettings.Password = protector.Unprotect(settings.Password);
                }
                catch
                {
                    _logger.LogError("The Yahoo password could not be decrypted. It may have been encrypted using a different key.");
                }
            }
        }
    }
}
