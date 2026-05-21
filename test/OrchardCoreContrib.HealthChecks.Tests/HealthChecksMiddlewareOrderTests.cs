using Moq;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

public class HealthChecksMiddlewareOrderTests
{
    [Fact]
    public async Task CorrectOrder_BlockedIp_ShouldReturn403_WithoutConsumingLimit()
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        // Act & Assert
        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "192.168.1.100");

        var httpResponse = await context.Client.GetAsync("health");
        
        Assert.Equal(HttpStatusCode.Forbidden, httpResponse.StatusCode);

        context.Client.DefaultRequestHeaders.Remove("X-Forwarded-For");
        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");

        for (int i = 1; i <= 5; i++)
        {
            httpResponse = await context.Client.GetAsync("health");

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }

    [Fact]
    public void BlockingRateLimiting_ShouldRunBefore_RateLimiting()
    {
        // Arrange
        var shellConfiguration = Mock.Of<IShellConfiguration>();
        var blockingStartup = new BlockingRateLimitingStartup(shellConfiguration);
        var rateLimitingStartup = new RateLimitingStartup(shellConfiguration);

        // Act
        var blockingOrder = blockingStartup.Order;
        var rateLimitingOrder = rateLimitingStartup.Order;

        // Assert
        Assert.True(blockingOrder < rateLimitingOrder,
            $"BlockingRateLimiting order ({blockingOrder}) should be less than RateLimiting order ({rateLimitingOrder}).");
    }

    //[Fact]
    //public async Task BlockingMiddleware_ShouldRunBefore_RateLimitingMiddleware()
    //{
    //    var executed = new List<string>();

    //    var builder = WebApplication.CreateBuilder();
    //    builder.Services.AddOptions<HealthChecksBlockingRateLimitingOptions>()
    //        .Configure(o =>
    //        {
    //            o.PermitLimit = 1;
    //            o.Window = TimeSpan.FromSeconds(5);
    //            o.SegmentsPerWindow = 5;
    //            o.BlockDuration = TimeSpan.FromSeconds(10);
    //        });
    //    builder.Services.AddSingleton<IHealthCheckRateLimiter, HealthCheckRateLimiter>();

    //    var app = builder.Build();

    //    // Register blocking first
    //    app.Use(async (ctx, next) =>
    //    {
    //        executed.Add("Blocking");
    //        //var middleware = new HealthChecksBlockingRateLimitingMiddleware(next,
    //        //    ctx.RequestServices.GetRequiredService<IOptions<HealthChecksBlockingRateLimitingOptions>>(),
    //        //    ctx.RequestServices.GetRequiredService<IClock>());
    //        //await middleware.InvokeAsync(ctx);
    //    });

    //    // Register plain limiter second
    //    app.Use(async (ctx, next) =>
    //    {
    //        executed.Add("RateLimiting");
    //        //var middleware = new HealthChecksRateLimitingMiddleware(next,
    //        //    ctx.RequestServices.GetRequiredService<IHealthCheckRateLimiter>());
    //        //await middleware.InvokeAsync(ctx);
    //    });

    //    app.Map("/health", () => Results.Ok("Healthy"));

    //    var client = app.CreateClient();
    //    var response = await client.GetAsync("/health");

    //    // Assert pipeline order
    //    Assert.Equal("Blocking", executed[0]);
    //    Assert.Equal("RateLimiting", executed[1]);

    //    // Also assert response is OK for first request
    //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //}
}
