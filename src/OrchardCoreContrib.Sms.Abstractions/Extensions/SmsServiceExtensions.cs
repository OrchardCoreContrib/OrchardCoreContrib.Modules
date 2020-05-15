using System;
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
            if (smsService is null)
            {
                throw new ArgumentNullException(nameof(smsService));
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("The phone number can't be null or empty.", nameof(phoneNumber));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("The text can't be null or empty.", nameof(text));
            }

            var message = new SmsMessage
            {
                PhoneNumber = phoneNumber,
                Text = text
            };

            return await smsService.SendAsync(message);
        }
    }
}
