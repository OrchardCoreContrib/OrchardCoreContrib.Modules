using System.Threading.Tasks;

namespace OrchardCoreContrib.Sms;

/// <summary>
/// Represents a contract for SMS service.
/// </summary>
public interface ISmsService
{
    /// <summary>
    /// Sends the specified message to an SMTP server for delivery.
    /// </summary>
    /// <param name="message">The message to be sent.</param>
    /// <returns>A <see cref="SmtpResult"/> that holds information about the sent message, for instance if it has sent successfully or if it has failed.</returns>
    Task<SmsResult> SendAsync(SmsMessage message);
}
