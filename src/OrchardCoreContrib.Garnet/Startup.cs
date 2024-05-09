using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represensts a startup point to register the required services by Garnet module.
/// </summary>
public class Startup(IShellConfiguration configuration) : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGarnetService, GarnetService>();
        services.AddSingleton<IModularTenantEvents>(sp => sp.GetRequiredService<IGarnetService>());
        services.AddSingleton<IGarnetClientFactory, GarnetClientFactory>();

        services.Configure<GarnetOptions>(configuration.GetSection(Constants.ConfigurationSectionName));
    }
}
