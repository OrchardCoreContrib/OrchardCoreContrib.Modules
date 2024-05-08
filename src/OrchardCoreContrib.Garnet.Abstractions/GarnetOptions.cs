namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a programable options for Garnet client.
/// </summary>
public class GarnetOptions
{
    /// <summary>
    /// Gets or sets the server host name or IP. Defaults to <c>"127.0.0.1"</c>.
    /// </summary>
    public string Host { get; set; } = "127.0.0.1";

    /// <summary>
    /// Gets or sets the server port. Defaults to <c>3278</c>.
    /// </summary>
    public int Port { get; set; } = 3278;

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or set the prefix that allowing a Garnet instance to be shared.
    /// </summary>
    public string InstancePrefix { get; set; }
}
