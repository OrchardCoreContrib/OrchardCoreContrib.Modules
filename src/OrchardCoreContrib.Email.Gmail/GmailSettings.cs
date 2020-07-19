using OrchardCore.Email;

namespace OrchardCoreContrib.Email.Gmail
{
    /// <summary>
    /// Represents a settings for Gmail.
    /// </summary>
    public class GmailSettings : SmtpSettings
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GmailSettings"/>.
        /// </summary>
        public GmailSettings()
        {
            Host = GmailDefaults.Host;
            Port = GmailDefaults.Port;
        }
    }
}
