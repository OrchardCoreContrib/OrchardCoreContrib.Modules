namespace Microsoft.AspNetCore.Http;

internal static class HttpReponseExtensions
{
    private const string AccessDeniedPath = "/Error/403";

    public static void RedirectToAccessDeniedPage(this HttpResponse httpResponse)
    {

        // TODO: Check why OC doesn't respects the cookie authentication options
        //var cookieAuthenticationOptions = httpResponse.HttpContext.RequestServices
        //    .GetService<IOptions<CookieAuthenticationOptions>>().Value;

        httpResponse.StatusCode = StatusCodes.Status403Forbidden;
        httpResponse.Redirect(AccessDeniedPath);
    }
}
