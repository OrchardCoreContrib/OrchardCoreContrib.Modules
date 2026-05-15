using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Configures IP-based access restrictions for health check endpoints during application startup.
/// </summary>
/// <param name="shellConfiguration">The <see cref="IShellConfiguration"/>.</param>
[Feature("OrchardCoreContrib.HealthChecks.IPRestriction")]
public class IPRestrictionStartup(IShellConfiguration shellConfiguration) : StartupBase
{
    /// <inheritdoc/>
    public override int Order => 10;

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
        => services.Configure<HealthChecksAccessOptions>(shellConfiguration.GetSection(Constants.HealthChecksAccessConfigurationKey));

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        => app.UseMiddleware<HealthChecksIPRestrictionMiddleware>();
}
