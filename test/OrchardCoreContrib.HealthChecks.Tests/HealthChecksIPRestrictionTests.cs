using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

public class HealthChecksIPRestrictionTests
{
    [Theory]
    [InlineData("10.0.0.1", HttpStatusCode.Forbidden)]
    [InlineData("127.0.0.1", HttpStatusCode.OK)]
    public async Task HealthCheck_RestrictIP_IfClientIPNotInAllowedIPs(string clientIP, HttpStatusCode expectedStatus)
    {
        // Arrange
        using var context = new SaasSiteContext();

        await context.InitializeAsync();

        // Act
        context.Client.DefaultRequestHeaders.Add("X-Forwarded-For", clientIP);

        var httpResponse = await context.Client.GetAsync("health");

        // Assert
        Assert.Equal(expectedStatus, httpResponse.StatusCode);
    }
}
