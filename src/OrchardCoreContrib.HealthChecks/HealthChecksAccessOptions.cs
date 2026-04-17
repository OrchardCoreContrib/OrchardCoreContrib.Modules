namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksAccessOptions
{
    public HashSet<string> AllowedIPs { get; set; } = [];
}
