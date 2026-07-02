using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using OrchardCoreContrib.Ban.Services;

namespace OrchardCoreContrib.Ban;

public class IPBanMiddleware(
    RequestDelegate next,
    ILogger<IPBanMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IIPBanService ipBanService)
    {
        // Skip middleware if this is a re-executed request (e.g., rendering a 404 error page)
        // This prevents infinite loops if the redirectUrl doesn't exist in the CMS.
        if (context.Features.Get<Microsoft.AspNetCore.Diagnostics.IStatusCodeReExecuteFeature>() != null)
        {
            await next(context);
            return;
        }

        var ip = context.Connection.RemoteIpAddress;
        if (ip is not null && await ipBanService.IsBannedAsync(ip))
        {
            var redirectUrl = await ipBanService.GetRedirectUrlAsync();

            if (!string.IsNullOrEmpty(redirectUrl) && RedirectHttpResult.IsLocalUrl(redirectUrl))
            {
                // Prevent infinite loop if already on the redirect page
                if (context.Request.Path.Equals(redirectUrl, StringComparison.OrdinalIgnoreCase))
                {
                    await next(context);
                    
                    return;
                }

                logger.LogWarning("Blocked request from banned IP: {IP}. Redirecting to {RedirectUrl}", ip, redirectUrl);
                
                context.Response.Redirect(redirectUrl);
                
                return;
            }

            logger.LogWarning("Blocked request from banned IP: {IP}", ip);
            
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied.");

            return;
        }

        await next(context);
    }
}
