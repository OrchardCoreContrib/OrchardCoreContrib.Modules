using OrchardCoreContrib.HealthChecks.Tests.Tests;
using System.Net;

namespace OrchardCoreContrib.HealthChecks.Tests;

public class IPRestrictionTests
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
        using var request = new HttpRequestMessage(HttpMethod.Get, "health");

        request.Headers.TryAddWithoutValidation("X-Forwarded-For", clientIP);

        var httpResponse = await context.Client.SendAsync(request);

        // Assert
        Assert.Equal(expectedStatus, httpResponse.StatusCode);
    }
}
