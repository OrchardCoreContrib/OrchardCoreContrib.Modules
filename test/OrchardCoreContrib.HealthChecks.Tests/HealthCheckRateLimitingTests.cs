using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

public class HealthCheckRateLimitingTests
{
    [Fact]
    public async Task ExceedingLimit_Returns429()
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        // Act
        HttpResponseMessage httpResponse = null;

        for (int i = 0; i < 6; i++)
        {
            httpResponse = await context.Client.GetAsync("health");
        }

        // Assert
        Assert.Equal(HttpStatusCode.TooManyRequests, httpResponse.StatusCode);

        var body = await httpResponse.Content.ReadAsStringAsync();

        Assert.Equal("Too Many Requests.", body);
    }
}
