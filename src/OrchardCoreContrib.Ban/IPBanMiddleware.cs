using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrchardCoreContrib.Ban.Services;

namespace OrchardCoreContrib.Ban;

public class IPBanMiddleware(
    RequestDelegate next,
    IIPBanService ipBanService,
    ILogger<IPBanMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress;
        if (ip is not null && await ipBanService.IsBannedAsync(ip))
        {
            logger.LogWarning("Blocked request from banned IP: {IP}", ip);

            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            await context.Response.WriteAsync("Access denied.");

            return;
        }

        await next(context);
    }
}
