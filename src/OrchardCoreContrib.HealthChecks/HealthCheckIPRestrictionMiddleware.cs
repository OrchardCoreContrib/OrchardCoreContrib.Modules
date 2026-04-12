using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell.Configuration;

namespace OrchardCoreContrib.HealthChecks;

public class HealthCheckIPRestrictionMiddleware(
    RequestDelegate next,
    IShellConfiguration shellConfiguration,
    IOptions<HealthChecksOptions> healthChecksOptions,
    ILogger<HealthCheckIPRestrictionMiddleware> logger)
{
    private readonly HealthChecksOptions _healthChecksOptions = healthChecksOptions.Value;
    private readonly HashSet<string> _allowedIPs =
        shellConfiguration.GetSection($"{Constants.ConfigurationKey}:AllowedIPs").Get<string[]>()?.ToHashSet(StringComparer.OrdinalIgnoreCase)
        ?? [];

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals(_healthChecksOptions.Url))
        {
            var remoteIP = context.Connection.RemoteIpAddress?.ToString();
            if (!_allowedIPs.Contains(remoteIP))
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
