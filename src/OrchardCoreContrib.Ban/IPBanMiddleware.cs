using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCoreContrib.Ban.Services;

namespace OrchardCoreContrib.Ban;

public class IPBanMiddleware(
    RequestDelegate next,
    IIPBanService ipBanService,
    IOptions<AdminOptions> adminOptions,
    ILogger<IPBanMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Prevent redirecting to a URL that doesn't exist
        var statusCodeReExecuteFeature = context.Features.Get<IStatusCodeReExecuteFeature>();
        if (statusCodeReExecuteFeature?.OriginalStatusCode == 404)
        {
            await next(context);

            return;
        }

        // Skip admin requests
        if (context.Request.Path.StartsWithSegments($"/{adminOptions.Value.AdminUrlPrefix}"))
        {
            await next(context);

            return;
        }

        var ip = context.Connection.RemoteIpAddress;
        if (await ipBanService.IsBannedAsync(ip))
        {
            var redirectUrl = await ipBanService.GetRedirectUrlAsync();

            logger.LogWarning("Blocked request from banned IP: {IP}", ip);

            if (string.IsNullOrEmpty(redirectUrl))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync("Access denied.");

                return;
            }
            else
            {
                // Avoid redirecting to BanSettings.RedirectUrl
                if (!context.Request.Path.Equals(redirectUrl, StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect(redirectUrl.TrimStart('/'));
                }
            }
        }

        await next(context);
    }
}
