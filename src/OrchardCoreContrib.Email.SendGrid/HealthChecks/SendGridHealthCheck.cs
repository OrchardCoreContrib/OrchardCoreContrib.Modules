using Microsoft.Extensions.Diagnostics.HealthChecks;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net;
using Microsoft.Extensions.Options;

namespace OrchardCoreContrib.Email.SendGrid.HealthChecks;

public class SendGridHealthCheck : IHealthCheck
{
    internal const string Name = "SendGrid Health Check";

    private readonly SendGridSettings _sendGridSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public SendGridHealthCheck(IOptions<SendGridSettings> sendGridSettings, IHttpClientFactory httpClientFactory)
    {
        _sendGridSettings = sendGridSettings.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("SendGrid");

            var client = new SendGridClient(httpClient, _sendGridSettings.ApiKey);
            var message = CreateMessage();

            var response = await client.SendEmailAsync(message, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {
                return new HealthCheckResult(context.Registration.FailureStatus, "Sending an email to SendGrid using the sandbox mode is not responding.");
            }
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
        }
    }

    private SendGridMessage CreateMessage()
    {
        var from = new EmailAddress(_sendGridSettings.DefaultSender);
        var to = new EmailAddress(_sendGridSettings.DefaultSender);
        
        var message = MailHelper
            .CreateSingleEmail(from, to, "SendGrid Health Check", "This is a test email to check if the SendGrid service is healthy.", null);

        message.SetSandBoxMode(true);

        return message;
    }
}
