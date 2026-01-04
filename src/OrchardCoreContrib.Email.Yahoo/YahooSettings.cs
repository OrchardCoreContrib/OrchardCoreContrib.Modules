namespace OrchardCoreContrib.Email.Yahoo;

/// <summary>
/// Represents a settings for Yahoo.
/// </summary>
public class YahooSettings : SmtpSettings
{
    /// <summary>
    /// Initializes a new instance of <see cref="YahooSettings"/>.
    /// </summary>
    public YahooSettings()
    {
        Host = YahooDefaults.Host;
        Port = YahooDefaults.Port;
    }
}
