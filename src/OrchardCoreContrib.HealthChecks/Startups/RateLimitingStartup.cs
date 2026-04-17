using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Configures rate limiting for health check endpoints during application startup.
/// </summary>
/// <param name="shellConfiguration">The <see cref="IShellConfiguration"/>.</param>
[Feature("OrchardCoreContrib.HealthChecks.RateLimiting")]
public class RateLimitingStartup(IShellConfiguration shellConfiguration) : StartupBase
{
    /// <inheritdoc/>
    public override int Order => 30;

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
        => services.Configure<HealthChecksRateLimitingOptions>(shellConfiguration.GetSection(Constants.HealthChecksRateLimitingConfigurationKey));

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        => app.UseMiddleware<HealthChecksRateLimitingMiddleware>();
}
