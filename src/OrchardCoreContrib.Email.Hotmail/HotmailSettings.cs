using OrchardCore.Email;

namespace OrchardCoreContrib.Email.Hotmail
{
    /// <summary>
    /// Represents a settings for Hotmail.
    /// </summary>
    public class HotmailSettings : SmtpSettings
    {
        /// <summary>
        /// Initializes a new instance of <see cref="HotmailSettings"/>.
        /// </summary>
        public HotmailSettings()
        {
            Host = HotmailDefaults.Host;
            Port = HotmailDefaults.Port;
        }
    }
}
