using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Gmail;
using OrchardCoreContrib.Email.Gmail.Services;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Modules.Email.Gmail.Tests
{
    public class GmailServiceTests
    {
        [Fact(Skip = "Set Gmail user credentials and default sender before run this test.")]
        public async Task Send()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("8471ee66-1e1d-4acf-8ea8-5e03556514b0")
                .Build();
            var settings = new GmailSettings
            {
                DefaultSender = config["Email:From"],
                UserName = config["Email:Gmail:Username"],
                Password = config["Email:Gmail:Password"]
            };
            var logger = Mock.Of<ILogger<GmailService>>();
            var localizer = Mock.Of<IStringLocalizer<GmailService>>();
            var service = new GmailService(Options.Create(settings), logger, localizer);
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
