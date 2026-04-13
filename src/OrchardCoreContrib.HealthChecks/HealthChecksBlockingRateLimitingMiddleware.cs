using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace OrchardCoreContrib.HealthChecks;

public class HealthChecksBlockingRateLimitingMiddleware
{
    private static readonly ConcurrentDictionary<string, DateTime> _blockedIPs = new();

    private readonly RequestDelegate _next;
    private readonly HealthChecksOptions _healthChecksOptions;
    private readonly HealthChecksRateLimitingOptions _healthChecksRateLimitingOptions;
    private readonly HealthChecksBlockingRateLimitingOptions _healthChecksBlockingRateLimitingOptions;
    private readonly SlidingWindowRateLimiter _rateLimiter;
    private readonly ILogger _logger;

    public HealthChecksBlockingRateLimitingMiddleware(
        RequestDelegate next,
        IOptions<HealthChecksOptions> healthChecksOptions,
        IOptions<HealthChecksRateLimitingOptions> healthChecksRateLimitingOptions,
        IOptions<HealthChecksBlockingRateLimitingOptions> healthChecksBlockingRateLimitingOptions,
        ILogger<HealthChecksRateLimitingMiddleware> logger)
    {
        _next = next;
        _healthChecksOptions = healthChecksOptions.Value;
        _healthChecksRateLimitingOptions = healthChecksRateLimitingOptions.Value;
        _healthChecksBlockingRateLimitingOptions = healthChecksBlockingRateLimitingOptions.Value;
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

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals(_healthChecksOptions.Url))
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            if (_blockedIPs.TryGetValue(ip, out var blockedUntil))
            {
                if (DateTime.UtcNow < blockedUntil)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    await context.Response.WriteAsync("Blocked due to excessive requests");

                    return;
                }
                else
                {
                    _blockedIPs.TryRemove(ip, out _);
                }
            }

            var rateLimitLease = _rateLimiter.AttemptAcquire(1);

            if (!rateLimitLease.IsAcquired)
            {
                _blockedIPs[ip] = DateTime.UtcNow.Add(_healthChecksBlockingRateLimitingOptions.BlockDuration);

                _logger.LogWarning("Rate limit exceeded for IP Address {RemoteIP}.", context.Connection.RemoteIpAddress);

                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await context.Response.WriteAsync("Too Many Requests.");

                return;
            }
        }

        await _next(context);
    }
}

