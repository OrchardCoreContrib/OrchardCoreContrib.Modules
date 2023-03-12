using OrchardCoreContrib.Email.SendGrid.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection;

public static class SendGridHealthCheckExtensions
{
    public static IHealthChecksBuilder AddSendGridCheck(this IHealthChecksBuilder healthChecksBuilder)
        => healthChecksBuilder.AddCheck<SendGridHealthCheck>(SendGridHealthCheck.Name);
}
