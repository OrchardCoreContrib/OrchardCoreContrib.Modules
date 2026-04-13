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
}
