using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Email;
using OrchardCoreContrib.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;

namespace OrchardCoreContrib.Email.SendGrid.Services;

/// <summary>
/// Represents a service for sending emails using SendGrid emailing service.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="SendGridService"/>.
/// </remarks>
/// <param name="sendGridSetting">The <see cref="IOptions<GmailSettings>"/>.</param>
/// <param name="stringLocalizer">The <see cref="IStringLocalizer<GmailService>"/>.</param>
public partial class SendGridService(
    IOptions<SendGridSettings> sendGridSetting,
    IStringLocalizer<SendGridService> stringLocalizer) : IEmailService
{
    private static readonly char[] EmailsSeparator = [',', ';', ' '];

    private readonly SendGridSettings _sendGridSetting = sendGridSetting.Value;
    private readonly IStringLocalizer S = stringLocalizer;

    /// <inheritdoc/>
    public async Task<Result> SendAsync(MailMessage message)
    {
        if (_sendGridSetting?.DefaultSender == null)
        {
            return Result.Failed(S["SendGrid settings must be configured before an email can be sent."]);
        }

        try
        {
            message.From = string.IsNullOrWhiteSpace(message.From)
                ? _sendGridSetting.DefaultSender
                : message.From;

            var sendGridMessage = FromMailMessage(message);

            var client = new SendGridClient(_sendGridSetting.ApiKey);

            await client.SendEmailAsync(sendGridMessage);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failed(S["An error occurred while sending an email: '{0}'", ex.Message]);
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

        sendGridMessage.PlainTextContent = message.TextBody is null
            ? string.Empty
            : HtmlTagRegex().Replace(message.TextBody, string.Empty);
        sendGridMessage.HtmlContent = message.HtmlBody ?? string.Empty;

        return sendGridMessage;
    }

    [GeneratedRegex(@"<[^>]*>", RegexOptions.Compiled)]
    private static partial Regex HtmlTagRegex();
}
