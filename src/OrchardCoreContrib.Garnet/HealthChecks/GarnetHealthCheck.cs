using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet.HealthChecks;

/// <summary>
/// Represents a health check for the Garnet service.
/// </summary>
/// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
public class GarnetHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    /// <inheritdoc/>
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var garnetService = serviceProvider.GetService<IGarnetService>();
            if (garnetService == null)
            {
                return HealthCheckResult.Unhealthy(description: $"The service '{nameof(IGarnetService)}' isn't registered.");
            }

            if (garnetService.Client is null)
            {
                await garnetService.ConnectAsync();
            }

            if (garnetService.Client.IsConnected)
            {
                var result = await garnetService.Client.PingAsync();
                if (result == "PONG")
                {
                    return HealthCheckResult.Healthy();
                }
                else
                {
                    return HealthCheckResult.Unhealthy(description: "The Garnet server couldn't be reached and might be offline or have degraded performance.");
                }
            }
            else
            {
                return HealthCheckResult.Unhealthy(description: "Couldn't connect to the Garnet server.");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Retrieving the status of the Garnet service failed.", ex);
        }
    }
}
