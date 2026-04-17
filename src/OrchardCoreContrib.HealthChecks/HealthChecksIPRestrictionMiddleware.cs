using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksIPRestrictionMiddleware(
    RequestDelegate next,
    IOptions<HealthChecksOptions> healthChecksOptions,
    IOptions<HealthChecksAccessOptions> healthChecksAccessOptions,
    ILogger<HealthChecksIPRestrictionMiddleware> logger)
{
    private readonly HealthChecksOptions _healthChecksOptions = healthChecksOptions.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals(_healthChecksOptions.Url))
        {
            var remoteIP = context.Connection.RemoteIpAddress?.ToString();
            if (!healthChecksAccessOptions.Value.AllowedIPs.Contains(remoteIP))
            {
                logger.LogWarning("Unauthorized IP {IP} tried to access {HealthCheckEndpoint}.", remoteIP, _healthChecksOptions.Url);

                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync("Forbidden");

                return;
            }
        }

        await next(context);
    }
}
