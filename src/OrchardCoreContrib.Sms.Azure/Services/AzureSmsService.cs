using Azure.Communication.Sms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Sms.Azure.Services;

public class AzureSmsService : ISmsService
{
    private readonly AzureSmsSettings _azureSmsOptions;
    private readonly ILogger _logger;
    private readonly IStringLocalizer S;

    public AzureSmsService(
        IOptions<AzureSmsSettings> azureSmsOptions,
        ILogger<AzureSmsService> logger,
        IStringLocalizer<AzureSmsService> stringLocalizer)
    {
        _azureSmsOptions = azureSmsOptions.Value;
        _logger = logger;
        S = stringLocalizer;
    }

    public async Task<SmsResult> SendAsync(SmsMessage message)
    {
        Guard.ArgumentNotNull(message, nameof(message));

        _logger.LogDebug("Attempting to send SMS to {PhoneNumber}.", message.PhoneNumber);

        try
        {
            var client = new SmsClient(_azureSmsOptions.ConnectionString);
            var smsResult = await client.SendAsync(_azureSmsOptions.SenderPhoneNumber, message.PhoneNumber, message.Text);

            return SmsResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending an SMS using the Azure SMS.");

            return SmsResult.Failed(S["An error occurred while sending an SMS."]);
        }
    }
}
