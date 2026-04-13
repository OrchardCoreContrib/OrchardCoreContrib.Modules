namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksBlockingRateLimitingOptions : HealthChecksRateLimitingOptions
{
    public TimeSpan BlockDuration { get; set; } = TimeSpan.FromMinutes(1);
}
