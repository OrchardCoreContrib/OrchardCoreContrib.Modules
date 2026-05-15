namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Provides configuration options for rate limiting applied to health check endpoints.
/// </summary>
public class HealthChecksRateLimitingOptions
{
    /// <summary>
    /// Gets or sets the maximum number of concurrent permits that can be acquired. Defaults to 5.
    /// </summary>
    public int PermitLimit { get; set; } = 5;

    /// <summary>
    /// Gets or sets the time window for which the rate limiting is applied. Defaults to 10 seconds.
    /// </summary>
    public TimeSpan Window { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Gets or sets the number of segments the time window is divided into for rate limiting purposes. Defaults to 10.
    /// </summary>
    public int SegmentsPerWindow { get; set; } = 10;

    /// <summary>
    /// Gets or sets the maximum number of requests that can be queued when the rate limit is exceeded. Defaults to 0.
    /// </summary>
    public int QueueLimit { get; set; } = 0;
}

