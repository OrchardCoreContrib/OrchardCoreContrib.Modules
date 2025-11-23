using Microsoft.Extensions.Caching.Memory;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OrchardCoreContrib.System.Models;

namespace OrchardCoreContrib.System.Services;


/// <summary>
/// Provides functionality to retrieve available system updates and access Orchard Core assembly information for the
/// current application.
/// </summary>
/// <param name="systemInformation">The system information provider used to determine the current Orchard Core version.</param>
/// <param name="memoryCache">The memory cache instance used to store and retrieve update information for improved performance.</param>
public class SystemUpdateService(SystemInformation systemInformation, IMemoryCache memoryCache) : ISystemUpdateService
{
    private const string Key = "OC_Versions";

    /// <inheritdoc/>
    public async Task<IEnumerable<SystemUpdate>> GetUpdatesAsync()
    {
        if (!memoryCache.TryGetValue(Key, out IEnumerable<SystemUpdate> updates))
        {
            var repository = Repository.Factory.GetCoreV3(Constants.SystemUpdates.NugetPackageSource);

            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            var versions = await resource.GetAllVersionsAsync(
                Constants.SystemUpdates.OrchardCorePackageId,
                new SourceCacheContext(),
                NullLogger.Instance,
                CancellationToken.None);

            updates = versions
                .Where(v => v.Version > systemInformation.OrchardCoreVersion)
                .Select(v => new SystemUpdate(v))
                .Reverse();

            memoryCache.Set(Key, updates, DateTimeOffset.UtcNow.AddDays(1));
        }

        return updates;
    }
}
