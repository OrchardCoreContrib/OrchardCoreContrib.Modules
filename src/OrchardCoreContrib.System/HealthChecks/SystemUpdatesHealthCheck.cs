using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Localization;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.HealthChecks;

public class SystemUpdatesHealthCheck : IHealthCheck
{
    internal const string Name = "System Updates Health Check";
    
    private readonly SystemInformation _systemInformation;
    private readonly ISystemUpdateService _systemUpdateService;
    private readonly IStringLocalizer<SystemInformation> S;

    public SystemUpdatesHealthCheck(
        SystemInformation systemInformation,
        ISystemUpdateService systemUpdateService,
        IStringLocalizer<SystemInformation> stringLocalizer)
    {
        _systemInformation = systemInformation;
        _systemUpdateService = systemUpdateService;
        S = stringLocalizer;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var updates = await _systemUpdateService.GetUpdatesAsync();

        var updatesCount = updates.Count(u => u.Version > _systemInformation.OrchardCoreVersion);

        return updatesCount == 0
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy(S.Plural(updatesCount, "There's {0} update available.", "There're {0} updates available.", updatesCount));
    }
}
