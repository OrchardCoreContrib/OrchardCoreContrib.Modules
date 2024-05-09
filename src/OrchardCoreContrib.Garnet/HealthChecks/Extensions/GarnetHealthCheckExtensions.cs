using OrchardCoreContrib.Garnet;
using OrchardCoreContrib.Garnet.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides a set of extensions for <see cref="IHealthChecksBuilder"/> to add Garnet health checks.
/// </summary>
public static class GarnetHealthCheckExtensions
{
    /// <summary>
    /// Adds a Garnet health check.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/>.</param>
    public static IHealthChecksBuilder AddGarnetCheck(this IHealthChecksBuilder healthChecksBuilder)
        => healthChecksBuilder.AddCheck<GarnetHealthCheck>(Constants.HealthCheckName);
}
