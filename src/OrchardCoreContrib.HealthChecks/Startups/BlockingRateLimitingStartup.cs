using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCoreContrib.HealthChecks.Startups;

/// <summary>
/// Configures blocking rate limiting for health check endpoints during application startup.
/// </summary>
/// <param name="shellConfiguration">The <see cref="IShellConfiguration"/>.</param>
[Feature("OrchardCoreContrib.HealthChecks.BlockingRateLimiting")]
public class BlockingRateLimitingStartup(IShellConfiguration shellConfiguration) : StartupBase
{
    /// <inheritdoc/>
    public override int Order => 20;

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        var rateLimitingSection = shellConfiguration.GetSection(Constants.HealthChecksRateLimitingConfigurationKey);

        services.Configure<HealthChecksRateLimitingOptions>(rateLimitingSection);
        services.Configure<HealthChecksBlockingRateLimitingOptions>(rateLimitingSection);
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        => app.UseMiddleware<HealthChecksBlockingRateLimitingMiddleware>();
}
