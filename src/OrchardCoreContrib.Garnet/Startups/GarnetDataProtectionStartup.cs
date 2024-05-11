using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represensts a startup point to register the required services by Garnet data protection feature.
/// </summary>
[Feature("OrchardCoreContrib.Garnet.DataProtection")]
public class GarnetDataProtectionStartup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        if (services.Any(d => d.ServiceType == typeof(IGarnetService)))
        {
            services.AddTransient<IConfigureOptions<KeyManagementOptions>, GarnetKeyManagementOptionsSetup>();
        }
    }
}
