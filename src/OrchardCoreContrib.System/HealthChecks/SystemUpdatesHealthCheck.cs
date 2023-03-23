using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Localization;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.HealthChecks;

public class SystemUpdatesHealthCheck : IHealthCheck
{
    private const string Key = "OC_Versions";
    internal const string Name = "System Updates Health Check";
    
    private readonly SystemInformation _systemInformation;
    private readonly IMemoryCache _memoryCache;
    private readonly IStringLocalizer<SystemInformation> S;

    public SystemUpdatesHealthCheck(
        SystemInformation systemInformation,
        IMemoryCache memoryCache,
        IStringLocalizer<SystemInformation> stringLocalizer)
    {
        _systemInformation = systemInformation;
        _memoryCache = memoryCache;
        S = stringLocalizer;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var versions = await GetVersionsAsync();

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

    private async Task<IEnumerable<NuGetVersion>> GetVersionsAsync()
    {
        if (!_memoryCache.TryGetValue(Key, out IEnumerable<NuGetVersion> versions))
        {
            var repository = Repository.Factory.GetCoreV3(SystemUpdatesConstants.NugetPackageSource);

            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            versions = await resource.GetAllVersionsAsync(
                SystemUpdatesConstants.OrchardCorePackageId,
                new SourceCacheContext(),
                NullLogger.Instance,
                CancellationToken.None);

            _memoryCache.Set(Key, versions, DateTimeOffset.UtcNow.AddDays(1));
        }

        return versions;
    }
}
