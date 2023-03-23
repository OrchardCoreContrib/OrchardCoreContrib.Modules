using Microsoft.Extensions.Caching.Memory;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OrchardCoreContrib.System.Models;
using System.Reflection;

namespace OrchardCoreContrib.System.Services;

public class SystemUpdateService : ISystemUpdateService
{
    private const string Key = "OC_Versions";

    private readonly SystemInformation _systemInformation;
    private readonly IMemoryCache _memoryCache;

    public SystemUpdateService(SystemInformation systemInformation, IMemoryCache memoryCache)
    {
        _systemInformation = systemInformation;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<SystemUpdate>> GetUpdatesAsync()
    {
        if (!_memoryCache.TryGetValue(Key, out IEnumerable<SystemUpdate> updates))
        {
            var repository = Repository.Factory.GetCoreV3(SystemUpdatesConstants.NugetPackageSource);

            var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            var versions = await resource.GetAllVersionsAsync(
                SystemUpdatesConstants.OrchardCorePackageId,
                new SourceCacheContext(),
                NullLogger.Instance,
                CancellationToken.None);

            updates = versions
                .Where(v => v.Version > _systemInformation.OrchardCoreVersion)
                .Select(v => new SystemUpdate(v))
                .Reverse();

            _memoryCache.Set(Key, updates, DateTimeOffset.UtcNow.AddDays(1));
        }

        return updates;
    }

    public static IEnumerable<AssemblyName> OrchardCoreAssemblies => Assembly.GetEntryAssembly()
        .GetReferencedAssemblies()
        .Where(a => a.Name.StartsWith("OrchardCore") && !a.Name.StartsWith("OrchardCoreContrib"));
}
