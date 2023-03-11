using OrchardCoreContrib.Tenants.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class TenantsHealthCheckExtensions
{
    public static IHealthChecksBuilder AddTenantsCheck(this IHealthChecksBuilder healthChecksBuilder)
        => healthChecksBuilder.AddCheck<TenantsHealthCheck>(TenantsHealthCheck.Name);
}
