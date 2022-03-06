using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCoreContrib.Email.Services;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Yahoo.Services
{
    /// <summary>
    /// Represents a service for sending emails using Yahoo emailing service.
    /// </summary>
    public class YahooService : SmtpService
    {
        private readonly YahooSettings _yahooSetting;

        /// <summary>
        /// Initializes a new instance of <see cref="YahooService"/>.
        /// </summary>
        /// <param name="yahooSetting">The <see cref="IOptions<YahooSettings>"/>.</param>
        /// <param name="logger">The <see cref="ILogger<YahooService>"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer<YahooService>"/>.</param>
        public YahooService(
            IOptions<YahooSettings> yahooSetting,
            ILogger<YahooService> logger,
            IStringLocalizer<YahooService> stringLocalizer
            ) : base(yahooSetting, logger, stringLocalizer)
        {
            _yahooSetting = yahooSetting.Value;
        }

        /// <inheritdoc/>
        protected async override Task SendOnlineMessageAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = CertificateValidationCallback;
                
                await client.ConnectAsync(_yahooSetting.Host, _yahooSetting.Port, SecureSocketOptions.Auto);

                if (!string.IsNullOrWhiteSpace(_yahooSetting.UserName))
                {
                    await client.AuthenticateAsync(_yahooSetting.UserName, _yahooSetting.Password);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
