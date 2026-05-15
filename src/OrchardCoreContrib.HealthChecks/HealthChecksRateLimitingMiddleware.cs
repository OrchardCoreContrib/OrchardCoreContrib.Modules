using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

namespace OrchardCoreContrib.HealthChecks;

/// <summary>
/// Middleware that enforces rate limiting on health check endpoints to prevent excessive requests.
/// </summary>
public class HealthChecksRateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HealthChecksOptions _healthChecksOptions;
    private readonly HealthChecksRateLimitingOptions _healthChecksRateLimitingOptions;
    private readonly SlidingWindowRateLimiter _rateLimiter;
    private readonly ILogger _logger;

    public HealthChecksRateLimitingMiddleware(
        RequestDelegate next,
        IOptions<HealthChecksOptions> healthChecksOptions,
        IOptions<HealthChecksRateLimitingOptions> healthChecksRateLimitingOptions,
        ILogger<HealthChecksRateLimitingMiddleware> logger)
    {
        _next = next;
        _healthChecksOptions = healthChecksOptions.Value;
        _healthChecksRateLimitingOptions = healthChecksRateLimitingOptions.Value;
        _logger = logger;
        _rateLimiter = new(new SlidingWindowRateLimiterOptions
        {
            PermitLimit = _healthChecksRateLimitingOptions.PermitLimit,
            Window = _healthChecksRateLimitingOptions.Window,
            SegmentsPerWindow = _healthChecksRateLimitingOptions.SegmentsPerWindow,
            QueueLimit = _healthChecksRateLimitingOptions.QueueLimit,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    }

    /// <inheritdoc/>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals(_healthChecksOptions.Url))
        {
            var rateLimitLease = _rateLimiter.AttemptAcquire(1);

            if (!rateLimitLease.IsAcquired)
            {
                _logger.LogWarning("Rate limit exceeded for IP Address {RemoteIP}.", context.Connection.RemoteIpAddress);

                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await context.Response.WriteAsync("Too Many Requests.");

                return;
            }
        }

        await _next(context);
    }
}
