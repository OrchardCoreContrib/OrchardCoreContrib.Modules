namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Provides configuration options for blocking rate limiting applied to health check endpoints.
/// </summary>
public class HealthChecksBlockingRateLimitingOptions : HealthChecksRateLimitingOptions
{
    /// <summary>
    /// Gets or sets the duration for which a client is blocked when the rate limit is exceeded. Defaults to 1 minute.
    /// </summary>
    public TimeSpan BlockDuration { get; set; } = TimeSpan.FromMinutes(1);
}
