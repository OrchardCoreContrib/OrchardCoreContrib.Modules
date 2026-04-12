using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksRateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HealthChecksOptions _healthChecksOptions;
    private readonly SlidingWindowRateLimiter _rateLimiter;
    private readonly ILogger _logger;

    public HealthChecksRateLimitingMiddleware(
        RequestDelegate next,
        IOptions<HealthChecksOptions> healthChecksOptions,
        IOptions<HealthChecksRateLimitingOptions> healthChecksRateLimitingOptions,
        ILogger<HealthChecksRateLimitingMiddleware> logger)
    {
        var healthChecksRateLimitingOptionsValue = healthChecksRateLimitingOptions.Value;
        _rateLimiter = new(new SlidingWindowRateLimiterOptions
        {
            PermitLimit = healthChecksRateLimitingOptionsValue.PermitLimit,
            Window = healthChecksRateLimitingOptionsValue.Window,
            SegmentsPerWindow = healthChecksRateLimitingOptionsValue.SegmentsPerWindow,
            QueueLimit = healthChecksRateLimitingOptionsValue.QueueLimit,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
        _next = next;
        _healthChecksOptions = healthChecksOptions.Value;
        _logger = logger;
    }

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
