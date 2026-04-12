using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

public class HealthChecksRateLimitingTests
{
    [Fact]
    public async Task ExceedingLimit_Returns429()
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");

        // Act
        HttpResponseMessage httpResponse = null;

        for (int i = 1; i <= 6; i++)
        {
            httpResponse = await context.Client.GetAsync("health");
        }

        // Assert
        Assert.Equal(HttpStatusCode.TooManyRequests, httpResponse.StatusCode);

        var body = await httpResponse.Content.ReadAsStringAsync();

        Assert.Equal("Too Many Requests.", body);
    }
}
