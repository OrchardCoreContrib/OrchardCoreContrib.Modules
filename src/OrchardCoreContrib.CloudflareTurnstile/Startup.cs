using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCoreContrib.CloudflareTurnstile.Configuration;
using OrchardCoreContrib.CloudflareTurnstile.Drivers;
using OrchardCoreContrib.CloudflareTurnstile.Services;
using Polly;

namespace OrchardCoreContrib.CloudflareTurnstile;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<TurnstileService>();
        services.AddShapeAttributes<TurnstileShape>();

        services
            .AddHttpClient(nameof(TurnstileService))
            .AddResilienceHandler("occ-handler", builder => builder
                .AddRetry(new HttpRetryStrategyOptions
                {
                    Name = "occ-retry",
                    MaxRetryAttempts = 3,
                    OnRetry = attempt =>
                    {
                        attempt.RetryDelay.Add(TimeSpan.FromSeconds(0.5 * attempt.AttemptNumber));

                        return ValueTask.CompletedTask;
                    },
                })
            );

        services.AddTransient<IConfigureOptions<TurnstileOptions>, TurnstileOptionsConfiguration>();

        services.AddSiteDisplayDriver<TurnstileSettingsDisplayDriver>();
        services.AddNavigationProvider<AdminMenu>();
        services.AddPermissionProvider<Permissions>();
    }
}

