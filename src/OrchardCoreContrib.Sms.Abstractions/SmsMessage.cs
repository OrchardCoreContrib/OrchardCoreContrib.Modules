namespace OrchardCoreContrib.Sms;

/// <summary>
/// Represents a class that contains an information for the sms message.
/// </summary>
public class SmsMessage
{
    /// <summary>
    /// Gets ot sets the phone number for the sender.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the actual message text to be sent.
    /// </summary>
    public string Text { get; set; }
}
