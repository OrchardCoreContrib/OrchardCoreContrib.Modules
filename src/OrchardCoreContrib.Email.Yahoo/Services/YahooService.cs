using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCoreContrib.Email.Services;

namespace OrchardCoreContrib.Email.Yahoo.Services;

/// <summary>
/// Represents a service for sending emails using Yahoo emailing service.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="YahooService"/>.
/// </remarks>
/// <param name="yahooSetting">The <see cref="IOptions<YahooSettings>"/>.</param>
/// <param name="logger">The <see cref="ILogger<YahooService>"/>.</param>
/// <param name="stringLocalizer">The <see cref="IStringLocalizer<YahooService>"/>.</param>
public class YahooService(
    IOptions<YahooSettings> yahooSetting,
    ILogger<YahooService> logger,
    IStringLocalizer<YahooService> stringLocalizer) : SmtpService(yahooSetting, logger, stringLocalizer)
{
    private readonly YahooSettings _yahooSetting = yahooSetting.Value;

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
