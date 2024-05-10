using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Cache;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represensts a startup point to register the required services by Garnet cache feature.
/// </summary>
[Feature("OrchardCoreContrib.Garnet.Cache")]
public class GarnetCacheStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        if (services.Any(d => d.ServiceType == typeof(IGarnetService)))
        {
            services.AddSingleton<IDistributedCache, GarnetCache>();
            services.AddScoped<ITagCache, GarnetTagCache>();
        }
    }
}
