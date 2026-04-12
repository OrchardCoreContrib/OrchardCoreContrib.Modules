namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksRateLimitingOptions
{
    public int PermitLimit { get; set; } = 5;

    public TimeSpan Window { get; set; } = TimeSpan.FromSeconds(10);

    public int SegmentsPerWindow { get; set; } = 10;

    public int QueueLimit { get; set; } = 0;
}

