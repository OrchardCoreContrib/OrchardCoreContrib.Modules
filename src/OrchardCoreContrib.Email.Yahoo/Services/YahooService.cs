using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using OrchardCore.Email;

namespace OrchardCoreContrib.Email.Yahoo.Services
{
    /// <summary>
    /// Represents a service for sending emails using Yahoo emailing service.
    /// </summary>
    public class YahooService : ISmtpService
    {
        private static readonly char[] EmailsSeparator = new char[] { ',', ';', ' ' };

        private readonly YahooSettings _yahooSetting;
        private readonly ILogger _logger;
        private readonly IStringLocalizer S;

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
            )
        {
            _yahooSetting = yahooSetting.Value;
            _logger = logger;
            S = stringLocalizer;
        }

        /// <inheritdoc/>
        public async Task<SmtpResult> SendAsync(MailMessage message)
        {
            if (_yahooSetting?.DefaultSender == null)
            {
                return SmtpResult.Failed(S["Yahoo settings must be configured before an email can be sent."]);
            }

            try
            {
                message.From = string.IsNullOrWhiteSpace(message.From)
                    ? _yahooSetting.DefaultSender
                    : message.From;

                var mimeMessage = FromMailMessage(message);

                await SendMessage(mimeMessage);

                return SmtpResult.Success;
            }
            catch (Exception ex)
            {
                return SmtpResult.Failed(S["An error occurred while sending an email: '{0}'", ex.Message]);
            }
        }

        private MimeMessage FromMailMessage(MailMessage message)
        {
            var senderAddress = string.IsNullOrWhiteSpace(message.Sender)
                ? _yahooSetting.DefaultSender
                : message.Sender;

            var mimeMessage = new MimeMessage
            {
                Sender = MailboxAddress.Parse(senderAddress)
            };

            mimeMessage.From.Add(MailboxAddress.Parse(message.From));

            if (!string.IsNullOrWhiteSpace(message.To))
            {
                foreach (var address in message.To.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.To.Add(MailboxAddress.Parse(address));
                }
            }

            if (!string.IsNullOrWhiteSpace(message.Cc))
            {
                foreach (var address in message.Cc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.Cc.Add(MailboxAddress.Parse(address));
                }
            }

            if (!string.IsNullOrWhiteSpace(message.Bcc))
            {
                foreach (var address in message.Bcc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.Bcc.Add(MailboxAddress.Parse(address));
                }
            }

            if (!string.IsNullOrWhiteSpace(message.ReplyTo))
            {
                foreach (var address in message.ReplyTo.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.ReplyTo.Add(MailboxAddress.Parse(address));
                }
            }

            mimeMessage.Subject = message.Subject;

            var body = new BodyBuilder();

            if (message.IsBodyHtml)
            {
                body.HtmlBody = message.Body;
            }
            else
            {
                body.TextBody = message.Body;
            }

            mimeMessage.Body = body.ToMessageBody();

            return mimeMessage;
        }

        private async Task SendMessage(MimeMessage message)
        {
            var secureSocketOptions = SecureSocketOptions.Auto;

            if (!_yahooSetting.AutoSelectEncryption)
            {
                switch (_yahooSetting.EncryptionMethod)
                {
                    case SmtpEncryptionMethod.None:
                        secureSocketOptions = SecureSocketOptions.None;
                        break;
                    case SmtpEncryptionMethod.SSLTLS:
                        secureSocketOptions = SecureSocketOptions.SslOnConnect;
                        break;
                    case SmtpEncryptionMethod.STARTTLS:
                        secureSocketOptions = SecureSocketOptions.StartTls;
                        break;
                    default:
                        break;
                }
            }

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = CertificateValidationCallback;
                await client.ConnectAsync(_yahooSetting.Host, _yahooSetting.Port, secureSocketOptions);

                if (_yahooSetting.RequireCredentials)
                {
                    if (_yahooSetting.UseDefaultCredentials)
                    {
                        await client.AuthenticateAsync(string.Empty, string.Empty);
                    }
                    else if (!string.IsNullOrWhiteSpace(_yahooSetting.UserName))
                    {
                        await client.AuthenticateAsync(_yahooSetting.UserName, _yahooSetting.Password);
                    }
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            _logger.LogError(string.Concat("Yahoo Server's certificate {CertificateSubject} issued by {CertificateIssuer} ",
                "with thumbprint {CertificateThumbprint} and expiration date {CertificateExpirationDate} ",
                "is considered invalid with {SslPolicyErrors} policy errors"),
                certificate.Subject, certificate.Issuer, certificate.GetCertHashString(),
                certificate.GetExpirationDateString(), sslPolicyErrors);

            if (sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors) && chain?.ChainStatus != null)
            {
                foreach (var chainStatus in chain.ChainStatus)
                {
                    _logger.LogError("Status: {Status} - {StatusInformation}", chainStatus.Status, chainStatus.StatusInformation);
                }
            }

            return false;
        }
    }
}
