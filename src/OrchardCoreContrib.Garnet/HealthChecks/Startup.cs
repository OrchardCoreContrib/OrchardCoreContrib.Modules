using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCoreContrib.HealthChecks;

namespace OrchardCoreContrib.Garnet.HealthChecks;

/// <summary>
/// Represensts a startup point to register the health checks for Garnet module.
/// </summary>
[RequireFeatures("OrchardCoreContrib.HealthChecks")]
public class Startup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHealthChecks()
            .AddGarnetCheck();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        var healthChecksOptions = serviceProvider.GetService<IOptions<HealthChecksOptions>>().Value;

        routes.MapHealthChecks($"{healthChecksOptions.Url}/garnet", new HealthCheckOptions
        {
            Predicate = r => r.Name == Constants.HealthCheckName
        });
    }
}
