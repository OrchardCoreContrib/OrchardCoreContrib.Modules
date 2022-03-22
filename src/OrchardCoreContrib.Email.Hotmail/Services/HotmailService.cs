using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCoreContrib.Email.Services;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Hotmail.Services
{
    /// <summary>
    /// Represents a service for sending emails using Hotmail emailing service.
    /// </summary>
    public class HotmailService : SmtpService
    {
        private readonly HotmailSettings _hotmailSetting;

        /// <summary>
        /// Initializes a new instance of <see cref="HotmailService"/>.
        /// </summary>
        /// <param name="hotmailSetting">The <see cref="IOptions<HotmailSettings>"/>.</param>
        /// <param name="logger">The <see cref="ILogger<HotmailService>"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer<HotmailService>"/>.</param>
        public HotmailService(
            IOptions<HotmailSettings> hotmailSetting,
            ILogger<HotmailService> logger,
            IStringLocalizer<HotmailService> stringLocalizer
            ) : base(hotmailSetting, logger, stringLocalizer)
        {
            _hotmailSetting = hotmailSetting.Value;
        }

        /// <inheritdoc/>
        protected async override Task SendOnlineMessageAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = CertificateValidationCallback;
                
                await client.ConnectAsync(_hotmailSetting.Host, _hotmailSetting.Port, SecureSocketOptions.StartTls);

                if (!string.IsNullOrWhiteSpace(_hotmailSetting.UserName))
                {
                    await client.AuthenticateAsync(_hotmailSetting.UserName, _hotmailSetting.Password);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
