using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Hotmail;
using OrchardCoreContrib.Email.Hotmail.Services;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Modules.Email.Hotmail.Tests
{
    public class HotmailServiceTests
    {
        [Fact(Skip = "Set Hotmail user credentials and default sender before run this test.")]
        public async Task Send()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("8471ee66-1e1d-4acf-8ea8-5e03556514b0")
                .Build();
            var settings = new HotmailSettings
            {
                DefaultSender = config["Email:From"],
                UserName = config["Email:Hotmail:Username"],
                Password = config["Email:Hotmail:Password"]
            };
            var logger = Mock.Of<ILogger<HotmailService>>();
            var localizer = Mock.Of<IStringLocalizer<HotmailService>>();
            var service = new HotmailService(Options.Create(settings), logger, localizer);
            var message = new MailMessage
            {
                To = config["Email:To"],
                Subject = "Test",
                Body = "Test message."
            };

            // Act
            var result = await service.SendAsync(message);

            // Assert
            Assert.True(result.Succeeded);
        }
    }
}
