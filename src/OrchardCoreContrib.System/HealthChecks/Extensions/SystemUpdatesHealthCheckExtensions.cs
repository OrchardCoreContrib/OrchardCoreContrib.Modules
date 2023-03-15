using OrchardCoreContrib.System.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class SystemUpdatesHealthCheckExtensions
{
    public static IHealthChecksBuilder AddSystemUpdatesCheck(this IHealthChecksBuilder healthChecksBuilder)
        => healthChecksBuilder.AddCheck<SystemUpdatesHealthCheck>(SystemUpdatesHealthCheck.Name);
}
