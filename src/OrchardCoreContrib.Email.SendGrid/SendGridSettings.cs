using OrchardCore.Email;

namespace OrchardCoreContrib.Email.SendGrid
{
    /// <summary>
    /// Represents a settings for SendGrid.
    /// </summary>
    public class SendGridSettings : SmtpSettings
    {
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
