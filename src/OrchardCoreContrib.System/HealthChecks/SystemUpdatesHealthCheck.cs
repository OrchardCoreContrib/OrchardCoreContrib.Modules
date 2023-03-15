using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Localization;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.HealthChecks;

public class SystemUpdatesHealthCheck : IHealthCheck
{
    internal const string Name = "System Updates Health Check";
    
    private readonly SystemInformation _systemInformation;
    private readonly IStringLocalizer<SystemInformation> S;

    public SystemUpdatesHealthCheck(SystemInformation systemInformation, IStringLocalizer<SystemInformation> stringLocalizer)
    {
        _systemInformation = systemInformation;
        S = stringLocalizer;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var repository = Repository.Factory.GetCoreV3(SystemUpdatesConstants.NugetPackageSource);

        var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

        var versions = await resource.GetAllVersionsAsync(
            SystemUpdatesConstants.OrchardCorePackageId,
            new SourceCacheContext(),
            NullLogger.Instance,
            CancellationToken.None);

        var updatesCount = versions.Count(v => v.Version > _systemInformation.OrchardCoreVersion);

        if (updatesCount == 0)
        {
            return HealthCheckResult.Healthy();
        }
        else
        {
            return HealthCheckResult.Unhealthy(S.Plural(updatesCount, "There's {0} update available.", "There're {0} updates available.", updatesCount));
        }
    }
}
