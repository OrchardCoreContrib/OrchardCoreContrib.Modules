using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Middleware that restricts access to health check endpoints based on allowed IP addresses.
/// </summary>
/// <param name="next">The <see cref="RequestDelegate"/> representing the next middleware in the pipeline.</param>
/// <param name="healthChecksOptions">The <see cref="HealthChecksOptions"/> containing health check configuration.</param>
/// <param name="healthChecksAccessOptions">The <see cref="HealthChecksAccessOptions"/> containing IP access configuration.</param>
/// <param name="logger">The <see cref="ILogger{HealthChecksIPRestrictionMiddleware}"/> used for logging.</param>
public class HealthChecksIPRestrictionMiddleware(
    RequestDelegate next,
    IOptions<HealthChecksOptions> healthChecksOptions,
    IOptions<HealthChecksAccessOptions> healthChecksAccessOptions,
    ILogger<HealthChecksIPRestrictionMiddleware> logger)
{
    private readonly HealthChecksOptions _healthChecksOptions = healthChecksOptions.Value;

    /// <inheritdoc/>
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
