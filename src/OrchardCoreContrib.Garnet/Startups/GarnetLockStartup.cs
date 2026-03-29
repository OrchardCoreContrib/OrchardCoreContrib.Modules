using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Locking.Distributed;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represensts a startup point to register the required services by Garnet lock feature.
/// </summary>
[Feature("OrchardCoreContrib.Garnet.Lock")]
public class GarnetLockStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        if (services.Any(d => d.ServiceType == typeof(IGarnetService)))
        {
            services.AddSingleton<IDistributedLock, GarnetLock>();
        }
    }
}
