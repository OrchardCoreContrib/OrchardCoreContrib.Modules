using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCoreContrib.HealthChecks.Models;
using System.Net.Mime;
using System.Text.Json;

namespace OrchardCoreContrib.HealthChecks;
public class Startup : StartupBase
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration)
    {
        _shellConfiguration = shellConfiguration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks();

        services.Configure<HealthChecksOptions>(_shellConfiguration.GetSection("OrchardCoreContrib_HealthChecks"));
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        var healthChecksOptions = serviceProvider.GetService<IOptions<HealthChecksOptions>>().Value;

        if (healthChecksOptions.ShowDetails)
        {
            routes.MapHealthChecks(healthChecksOptions.Url, new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = WriteResponse
            });
        }
        else
        {
            routes.MapHealthChecks(healthChecksOptions.Url);
        }
    }

    private static async Task WriteResponse(HttpContext context, HealthReport report)
    {
        var response = new HealthCheckReponse
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration,
            HealthChecks = report.Entries.Select(item => new HealthCheckEntry
            {
                Name = item.Key,
                Status = item.Value.Status.ToString(),
                Description = item.Value.Description
            })
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, response.GetType(), options: _jsonSerializerOptions));
    }
}
