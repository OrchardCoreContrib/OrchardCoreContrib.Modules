using Azure.Communication.Sms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Sms.Azure.Services;

public class AzureSmsService(
    IOptions<AzureSmsSettings> azureSmsOptions,
    ILogger<AzureSmsService> logger,
    IStringLocalizer<AzureSmsService> S) : ISmsService
{
    private readonly AzureSmsSettings _azureSmsOptions = azureSmsOptions.Value;

    public async Task<SmsResult> SendAsync(SmsMessage message)
    {
        Guard.ArgumentNotNull(message, nameof(message));

        logger.LogDebug("Attempting to send SMS to {PhoneNumber}.", message.PhoneNumber);

        try
        {
            var client = new SmsClient(_azureSmsOptions.ConnectionString);
            var smsResult = await client.SendAsync(_azureSmsOptions.SenderPhoneNumber, message.PhoneNumber, message.Text);

            return SmsResult.Success;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while sending an SMS using the Azure SMS.");

            return SmsResult.Failed(S["An error occurred while sending an SMS."]);
        }
    }
}
