using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Email.Hotmail.Services
{
    /// <summary>
    /// Represents a configuration for <see cref="HotmailSettings"/>.
    /// </summary>
    public class HotmailSettingsConfiguration : IConfigureOptions<HotmailSettings>
    {
        private readonly ISiteService _site;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="HotmailSettingsConfiguration"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISiteService"/>.</param>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="logger">The <see cref="ILogger<HotmailSettingsConfiguration>"/>.</param>
        public HotmailSettingsConfiguration(
            ISiteService site,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<HotmailSettingsConfiguration> logger)
        {
            _site = site;
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void Configure(HotmailSettings hotmailSettings)
        {
            var settings = _site.GetSiteSettingsAsync()
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
                    var protector = _dataProtectionProvider.CreateProtector(nameof(HotmailSettingsConfiguration));
                    hotmailSettings.Password = protector.Unprotect(settings.Password);
                }
                catch
                {
                    _logger.LogError("The Hotmail password could not be decrypted. It may have been encrypted using a different key.");
                }
            }
        }
    }
}
