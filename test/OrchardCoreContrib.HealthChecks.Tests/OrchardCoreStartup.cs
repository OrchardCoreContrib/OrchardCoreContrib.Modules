using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreContrib.Modules.Web;
using OrchardCoreContrib.Testing;
using OrchardCoreContrib.Testing.Security;

namespace OrchardCoreContrib.HealthChecks.Tests.Tests;

public class OrchardCoreStartup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOrchardCms(builder => builder
            .AddSetupFeatures("OrchardCore.Tenants")
            .AddTenantFeatures("OrchardCoreContrib.HealthChecks.IPRestriction", "OrchardCoreContrib.HealthChecks.RateLimiting", "OrchardCoreContrib.HealthChecks.BlockingRateLimiting")
            .ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddScoped<IAuthorizationHandler, PermissionContextAuthorizationHandler>(sp =>
                    new PermissionContextAuthorizationHandler(sp.GetRequiredService<IHttpContextAccessor>(), SiteContextOptions.PermissionsContexts));

                serviceCollection.AddSingleton<IConfiguration>(AddHealthChecksConfiguration());
            })
            .Configure(appBuilder => appBuilder.UseAuthorization()));

        services.AddSingleton<IModuleNamesProvider>(new ModuleNamesProvider(typeof(Program).Assembly));
    }

    public void Configure(IApplicationBuilder app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor
        };

        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(forwardedHeadersOptions);
        app.UseOrchardCore();
    }

    private IConfigurationRoot AddHealthChecksConfiguration()
    {
        var newConfiguration = new Dictionary<string, string>
        {
            { $"{Constants.ConfigurationKey}:{nameof(HealthChecksOptions.Url)}", "/health" },
            { $"{Constants.ConfigurationKey}:AllowedIPs:0", "127.0.0.1" },
            { $"{Constants.ConfigurationKey}:AllowedIPs:1", "::1" }
        };

        return new ConfigurationBuilder()
            .AddConfiguration(configuration)
            .AddInMemoryCollection(newConfiguration)
            .Build();
    }
}
