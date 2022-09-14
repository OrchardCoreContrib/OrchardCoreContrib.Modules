using MailKit.Net.Proxy;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Services;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Email.Mocks.Tests;

public class SmtpServiceMock : SmtpService
{
    private readonly SmtpSettings _options;

    public SmtpServiceMock(
        IOptions<SmtpSettings> options,
        ILogger<SmtpService> logger,
        IStringLocalizer<SmtpService> stringLocalizer) : base(options, logger, stringLocalizer)
    {
        _options = options.Value;
    }

    protected async override Task SendOnlineMessageAsync(MimeMessage message)
    {
        var secureSocketOptions = _options.AutoSelectEncryption
        ? SecureSocketOptions.Auto
        : _options.EncryptionMethod switch
        {
            SmtpEncryptionMethod.None => SecureSocketOptions.None,
            SmtpEncryptionMethod.SSLTLS => SecureSocketOptions.SslOnConnect,
            SmtpEncryptionMethod.STARTTLS => SecureSocketOptions.StartTls,
            _ => SecureSocketOptions.Auto
        };

        using (var client = new SmtpClient())
        {
            client.ProxyClient = new Socks5Client(_options.Proxy.Host, _options.Proxy.Port);

            await client.ConnectAsync(_options.Host, _options.Port, secureSocketOptions);

            Assert.True(client.IsConnected);
            Assert.True(client.IsSecure);
            Assert.True(client.IsEncrypted);
            Assert.False(client.IsAuthenticated);

            await client.DisconnectAsync(true);

            Assert.False(client.IsConnected);
        }
    }
}
