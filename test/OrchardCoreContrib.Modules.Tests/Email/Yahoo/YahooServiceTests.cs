using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Yahoo;
using OrchardCoreContrib.Email.Yahoo.Services;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Modules.Email.Yahoo.Tests
{
    public class YahooServiceTests
    {
        [Fact(Skip = "Set Yahoo user credentials and default sender before run this test. Also you might use Yahoo App password for authentication to make test pass.")]
        public async Task Send()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("8471ee66-1e1d-4acf-8ea8-5e03556514b0")
                .Build();
            var settings = new YahooSettings
            {
                DefaultSender = config["Email:From"],
                UserName = config["Email:Yahoo:Username"],
                Password = config["Email:Yahoo:Password"]
            };
            var logger = Mock.Of<ILogger<YahooService>>();
            var localizer = Mock.Of<IStringLocalizer<YahooService>>();
            var service = new YahooService(Options.Create(settings), logger, localizer);
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
