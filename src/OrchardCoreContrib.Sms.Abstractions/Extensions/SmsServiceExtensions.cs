using OrchardCoreContrib.Infrastructure;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Sms
{
    /// <summary>
    /// Provides an extension methods for <see cref="ISmsService"/>.
    /// </summary>
    public static class SmsServiceExtensions
    {
        /// <summary>
        /// Sends an SMS message using a provided <paramref name="phoneNumber"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="smsService">The <see cref="ISmsService"/>.</param>
        /// <param name="phoneNumber">The phone number for the sender.</param>
        /// <param name="text">The text message to be sent.</param>
        /// <returns>A <see cref="SmtpResult"/> that holds information about the sent message, for instance if it has sent successfully or if it has failed.</returns>
        public async static Task<SmsResult> SendAsync(this ISmsService smsService, string phoneNumber, string text)
        {
            Guard.ArgumentNotNull(smsService, nameof(smsService));
            Guard.ArgumentNotNullOrEmpty(phoneNumber, nameof(phoneNumber));
            Guard.ArgumentNotNullOrEmpty(text, nameof(text));

            var message = new SmsMessage
            {
                PhoneNumber = phoneNumber,
                Text = text
            };

            return await smsService.SendAsync(message);
        }
    }
}
