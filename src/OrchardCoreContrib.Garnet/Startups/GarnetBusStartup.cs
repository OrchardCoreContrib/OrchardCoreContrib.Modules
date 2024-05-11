using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Caching.Distributed;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represensts a startup point to register the required services by Garnet bus feature.
/// </summary>
[Feature("OrchardCoreContrib.Garnet.Bus")]
public class GarnetBusStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        if (services.Any(d => d.ServiceType == typeof(IGarnetService)))
        {
            services.AddSingleton<IMessageBus, GarnetBus>();
        }
    }
}
