namespace OrchardCoreContrib.Email;

/// <summary>
/// Represents a settings for SMTP
/// </summary>
public class SmtpSettings : OrchardCore.Email.SmtpSettings
{
    /// <summary>
    /// Gets or sets Proxy server that could be used in mailing services.
    /// </summary>
    public MailProxy Proxy { get; set; }
}
