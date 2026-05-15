using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement.Routing;
using OrchardCore.Settings;
using OrchardCore.Users;
using System.Net;
using System.Net.Mime;

namespace OrchardCoreContrib.System.Services;

/// <summary>
/// Middleware that redirects unauthenticated users to a maintenance page when maintenance mode is enabled.
/// </summary>
public class MaintenanceRedirectMiddleware(
    RequestDelegate next,
    ISiteService siteService,
    IOptions<AdminOptions> adminOptions,
    IOptions<UserOptions> userOptions,
    IAutorouteEntries autorouteEntries,
    IStringLocalizer<MaintenanceRedirectMiddleware> S,
    ILogger<MaintenanceRedirectMiddleware> logger)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    private readonly UserOptions _userOptions = userOptions.Value;

    /// <inheritdoc/>
    /// <param name="context">The <see cref="HttpContext"/> representing the current HTTP request and response.</param>
    /// <param name="next">The next middleware delegate to invoke in the request pipeline.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    /// <exception cref="NotImplementedException">Thrown in all cases, as the method is not implemented.</exception>
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        if (context.User.Identity.IsAuthenticated ||
            path.StartsWith($"/{_userOptions.LoginPath}", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith($"/{_adminOptions.AdminUrlPrefix}", StringComparison.OrdinalIgnoreCase) ||
            path.Equals(Constants.SystemMaintenance.MaintenancePath, StringComparison.OrdinalIgnoreCase))
        {
            await next(context);

            return;
        }

        var systemSettings = (await siteService.GetSiteSettingsAsync()).As<SystemSettings>();

        if (systemSettings.AllowMaintenanceMode)
        {
            logger.LogInformation(S["Maintenance mode is on."]);

            (var found, _) = await autorouteEntries.TryGetEntryByPathAsync(Constants.SystemMaintenance.MaintenancePath);

            if (found)
            {
                context.Response.Redirect(context.Request.PathBase + Constants.SystemMaintenance.MaintenancePath);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                context.Response.ContentType = MediaTypeNames.Text.Html;

                await context.Response.WriteAsync(Constants.SystemMaintenance.DefaultMaintenancePageContent);
            }

            return;
        }

        await next(context);
    }
}
