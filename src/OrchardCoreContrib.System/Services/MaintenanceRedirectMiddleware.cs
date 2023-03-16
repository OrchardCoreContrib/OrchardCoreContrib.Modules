using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement.Routing;
using OrchardCore.Entities;
using OrchardCore.Settings;
using OrchardCore.Users;
using System.Net;

namespace OrchardCoreContrib.System.Services;

public class MaintenanceRedirectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISiteService _siteService;
    private readonly AdminOptions _adminOptions;
    private readonly UserOptions _userOptions;
    private readonly IAutorouteEntries _autorouteEntries;
    private readonly ILogger<MaintenanceRedirectMiddleware> _logger;

    public MaintenanceRedirectMiddleware(
        RequestDelegate next,
        ISiteService siteService,
        IOptions<AdminOptions> adminOptions,
        IOptions<UserOptions> userOptions,
        IAutorouteEntries autorouteEntries,
        ILogger<MaintenanceRedirectMiddleware> logger)
    {
        _next = next;
        _siteService = siteService;
        _adminOptions = adminOptions.Value;
        _userOptions = userOptions.Value;
        _autorouteEntries = autorouteEntries;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        if (context.User.Identity.IsAuthenticated ||
            path.StartsWith($"/{_userOptions.LoginPath}", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWith($"/{_adminOptions.AdminUrlPrefix}", StringComparison.OrdinalIgnoreCase) ||
            path.Equals(SystemMaintenanceConstants.MaintenancePath, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);

            return;
        }

        var systemSettings = (await _siteService.GetSiteSettingsAsync()).As<SystemSettings>();

        if (systemSettings.AllowMaintenanceMode)
        {
            _logger.LogInformation("Maintenance mode is on.");

            (var found, _) = await _autorouteEntries.TryGetEntryByPathAsync(SystemMaintenanceConstants.MaintenancePath);

            if (found)
            {
                context.Response.Redirect(context.Request.PathBase + SystemMaintenanceConstants.MaintenancePath);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                context.Response.ContentType = "text/html";

                await context.Response.WriteAsync(SystemMaintenanceConstants.DefaultMaintenancePageContent);
            }

            return;
        }

        await _next(context);
    }
}
