using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

[Collection("Sequential")]
public class HealthCheckBlockingRateLimitingTests
{
    [Fact]
    public async Task ExceedingLimit_ShouldBlockIP_ForConfiguredDuration()
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");

        HttpResponseMessage response = null;

        // Act
        for (int i = 1; i <= 6; i++)
        {
            response = await context.Client.GetAsync("health");
        }

        // Assert
        Assert.Equal(HttpStatusCode.TooManyRequests, response.StatusCode);

        response = await context.Client.GetAsync("health");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("Blocked due to excessive requests", body);
    }

    [Fact]
    public async Task BlockExpires_AfterDuration_AllowsRequestsAgain()
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");

        // Act
        for (int i = 1; i <= 6; i++)
        {
            await context.Client.GetAsync("health");
        }

        var response = await context.Client.GetAsync("health");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        // Wait slightly longer than block duration
        await Task.Delay(TimeSpan.FromSeconds(11));

        response = await context.Client.GetAsync("health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
