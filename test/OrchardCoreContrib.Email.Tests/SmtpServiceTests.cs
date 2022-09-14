using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Mocks.Tests;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Email.Tests;

public partial class SmtpServiceTests
{
    [Fact]
    public async Task ConnectSmtpViaProxy()
    {
        // Arrange
        var settings = new SmtpSettings
        {
            Host = "smtp.gmail.com",
            Port = 0,
            EncryptionMethod = SmtpEncryptionMethod.SSLTLS
        };
        var logger = Mock.Of<ILogger<SmtpServiceMock>>();
        var localizer = Mock.Of<IStringLocalizer<SmtpServiceMock>>();

        var message = new MailMessage
        {
            From = "occ@gmail.com",
            To = "hishambinateya@gmail.com",
            Subject = "Test",
            Body = "Test message."
        };

        // Act & Assert
        using (var proxy = new Socks5ProxyListener())
        {
            proxy.Start(IPAddress.Loopback, 0);

            settings.Proxy = new MailProxy
            {
                Host = proxy.IPAddress.ToString(),
                Port = proxy.Port
            };

            var smtpService = new SmtpServiceMock(Options.Create(settings), logger, localizer);

            var result = await smtpService.SendAsync(message);

            Assert.True(result.Succeeded);
        }
    }
}
