using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Email;
using OrchardCoreContrib.Email.SendGrid;
using OrchardCoreContrib.Email.SendGrid.Services;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Modules.Email.SendGrid.Tests
{
    public class SendGridServiceTests
    {
        [Fact(Skip = "Set SendGrid API key and default sender before run this test.")]
        public async Task Send()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("8471ee66-1e1d-4acf-8ea8-5e03556514b0")
                .Build();
            var settings = new SendGridSettings
            {
                DefaultSender = config["Email:From"],
                ApiKey = config["Email:SendGrid:ApiKey"]
            };
            var localizer = Mock.Of<IStringLocalizer<SendGridService>>();
            var service = new SendGridService(Options.Create(settings), localizer);
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
