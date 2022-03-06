﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCoreContrib.Email.Services;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Gmail.Services
{
    /// <summary>
    /// Represents a service for sending emails using Gmail emailing service.
    /// </summary>
    public class GmailService : SmtpService
    {
        private readonly GmailSettings _gmailSetting;

        /// <summary>
        /// Initializes a new instance of <see cref="GmailService"/>.
        /// </summary>
        /// <param name="gmailSetting">The <see cref="IOptions<GmailSettings>"/>.</param>
        /// <param name="logger">The <see cref="ILogger<GmailService>"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer<GmailService>"/>.</param>
        public GmailService(
            IOptions<GmailSettings> gmailSetting,
            ILogger<GmailService> logger,
            IStringLocalizer<GmailService> stringLocalizer
            ) : base(gmailSetting, logger, stringLocalizer)
        {
            _gmailSetting = gmailSetting.Value;
        }

        /// <inheritdoc/>
        protected async override Task SendOnlineMessageAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = CertificateValidationCallback;
                
                await client.ConnectAsync(_gmailSetting.Host, _gmailSetting.Port, SecureSocketOptions.StartTls);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(_gmailSetting.UserName))
                {
                    await client.AuthenticateAsync(_gmailSetting.UserName, _gmailSetting.Password);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
