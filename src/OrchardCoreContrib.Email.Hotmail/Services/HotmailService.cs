using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCoreContrib.Email.Services;

namespace OrchardCoreContrib.Email.Hotmail.Services;

/// <summary>
/// Represents a service for sending emails using Hotmail emailing service.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="HotmailService"/>.
/// </remarks>
/// <param name="hotmailSetting">The <see cref="IOptions<HotmailSettings>"/>.</param>
/// <param name="logger">The <see cref="ILogger<HotmailService>"/>.</param>
/// <param name="stringLocalizer">The <see cref="IStringLocalizer<HotmailService>"/>.</param>
public class HotmailService(
    IOptions<HotmailSettings> hotmailSetting,
    ILogger<HotmailService> logger,
    IStringLocalizer<HotmailService> stringLocalizer) : SmtpService(hotmailSetting, logger, stringLocalizer)
{
    private readonly HotmailSettings _hotmailSetting = hotmailSetting.Value;

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
