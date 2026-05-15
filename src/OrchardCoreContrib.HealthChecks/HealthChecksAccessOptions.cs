namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Provides configuration options for controlling access to health check endpoints, such as allowed IP addresses.
/// </summary>
public class HealthChecksAccessOptions
{
    /// <summary>
    /// Gets or sets the collection of IP addresses that are permitted access.
    /// </summary>
    public HashSet<string> AllowedIPs { get; set; } = [];
}
