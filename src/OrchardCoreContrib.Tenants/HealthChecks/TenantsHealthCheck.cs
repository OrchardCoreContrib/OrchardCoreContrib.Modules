using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Models;

namespace OrchardCoreContrib.Tenants.HealthChecks;

public class TenantsHealthCheck(IShellHost shellHost, IServiceProvider serviceProvider) : IHealthCheck
{
    internal const string Name = "Tenants Health Check";

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var nonRunningTenants = shellHost.GetAllSettings()
            .Where(s => !s.IsDefaultShell() && s.State != TenantState.Running);
        if (nonRunningTenants.Any())
        {
            var description = String.Empty;
            var localizer = serviceProvider.GetService<IStringLocalizer<TenantsHealthCheck>>();

            if (nonRunningTenants.Count() == 1)
            {
                description = localizer["The tenant {0} is not running.", nonRunningTenants.Single().Name];
            }
            else
            {
                description = localizer["The tenants {0} are not running.", String.Join(", ", nonRunningTenants.Select(s => s.Name))];
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(description));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
