using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Http;

// TODO: Move this to OCC.Infrastructure.Abstractions
internal static class HttpReponseExtensions
{
    public static void RedirectToAccessDeniedPage(this HttpResponse httpResponse)
    {
        var cookieAuthenticationOptions = httpResponse.HttpContext.RequestServices
            .GetService<IOptionsMonitor<CookieAuthenticationOptions>>()
            .Get(IdentityConstants.ApplicationScheme);

        httpResponse.StatusCode = StatusCodes.Status403Forbidden;
        httpResponse.Redirect(cookieAuthenticationOptions.AccessDeniedPath);
    }
}
