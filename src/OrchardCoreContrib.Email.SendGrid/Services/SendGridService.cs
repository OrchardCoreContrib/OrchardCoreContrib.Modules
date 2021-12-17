using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OrchardCoreContrib.Email.SendGrid.Services
{
    /// <summary>
    /// Represents a service for sending emails using SendGrid emailing service.
    /// </summary>
    public class SendGridService : ISmtpService
    {
        private static readonly char[] EmailsSeparator = new char[] { ',', ';', ' ' };
        private static readonly Regex HtmlTagRegex = new Regex(@"<[^>]*>", RegexOptions.Compiled);

        private readonly SendGridSettings _sendGridSetting;
        private readonly IStringLocalizer S;

        /// <summary>
        /// Initializes a new instance of <see cref="SendGridService"/>.
        /// </summary>
        /// <param name="sendGridSetting">The <see cref="IOptions<GmailSettings>"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer<GmailService>"/>.</param>
        public SendGridService(
            IOptions<SendGridSettings> sendGridSetting,
            IStringLocalizer<SendGridService> stringLocalizer
            )
        {
            _sendGridSetting = sendGridSetting.Value;
            S = stringLocalizer;
        }

        /// <inheritdoc/>
        public async Task<SmtpResult> SendAsync(MailMessage message)
        {
            if (_sendGridSetting?.DefaultSender == null)
            {
                return SmtpResult.Failed(S["SendGrid settings must be configured before an email can be sent."]);
            }

            try
            {
                message.From = string.IsNullOrWhiteSpace(message.From)
                    ? _sendGridSetting.DefaultSender
                    : message.From;

                var sendGridMessage = FromMailMessage(message);

                var client = new SendGridClient(_sendGridSetting.ApiKey);
                
                await client.SendEmailAsync(sendGridMessage);

                return SmtpResult.Success;
            }
            catch (Exception ex)
            {
                return SmtpResult.Failed(S["An error occurred while sending an email: '{0}'", ex.Message]);
            }
        }

        private SendGridMessage FromMailMessage(MailMessage message)
        {
            var senderAddress = string.IsNullOrWhiteSpace(message.Sender)
                ? _sendGridSetting.DefaultSender
                : message.From;

            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(senderAddress)
            };

            if (!string.IsNullOrWhiteSpace(message.To))
            {
                foreach (var address in message.To.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    sendGridMessage.AddTo(new EmailAddress(address));
                }
            }

            if (!string.IsNullOrWhiteSpace(message.Cc))
            {
                foreach (var address in message.Cc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    sendGridMessage.AddCc(address);
                }
            }

            if (!string.IsNullOrWhiteSpace(message.Bcc))
            {
                foreach (var address in message.Bcc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    sendGridMessage.AddBcc(address);
                }
            }

            sendGridMessage.Subject = message.Subject;

            if (message.IsBodyHtml)
            {
                sendGridMessage.PlainTextContent = HtmlTagRegex.Replace(message.Body, String.Empty);
                sendGridMessage.HtmlContent = message.Body;
            }
            else
            {
                sendGridMessage.PlainTextContent = message.Body;
                sendGridMessage.HtmlContent = String.Empty;
            }

            return sendGridMessage;
        }
    }
}
