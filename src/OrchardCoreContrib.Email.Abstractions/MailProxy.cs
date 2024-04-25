namespace OrchardCoreContrib.Email;

/// <summary>
/// Represents a proxy server that could be used for mailing services.
/// </summary>
public class MailProxy
{
    /// <summary>
    /// Gets or sets the host name of the proxy server.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the proxy port.
    /// </summary>
    public int Port { get; set; }
}
