using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCoreContrib.HealthChecks;

namespace OrchardCoreContrib.Email.SendGrid.HealthChecks;

[RequireFeatures("OrchardCoreContrib.HealthChecks")]
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHealthChecks()
            .AddSendGridCheck();
    }

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        var healthChecksOptions = serviceProvider.GetService<IOptions<HealthChecksOptions>>().Value;

        routes.MapHealthChecks($"{healthChecksOptions.Url}/sendgrid", new HealthCheckOptions
        {
            Predicate = r => r.Name == SendGridHealthCheck.Name
        });
    }
}
