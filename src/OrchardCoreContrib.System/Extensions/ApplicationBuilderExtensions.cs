using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement.Routing;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCoreContrib.System.Services;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Provides extension methods for configuring maintenance redirect middleware in an ASP.NET Core application's request
/// pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware to the application's request pipeline that redirects users to a maintenance page when the site
    /// is in maintenance mode.
    /// </summary>
    /// <param name="app">The application builder used to configure the request pipeline. Cannot be null.</param>
    /// <returns>The application builder instance with the maintenance redirect middleware configured.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseMaintenanceRedirect(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var siteService = app.ApplicationServices.GetService<ISiteService>();
        var adminOptions = app.ApplicationServices.GetService<IOptions<AdminOptions>>();
        var userOptions = app.ApplicationServices.GetService<IOptions<UserOptions>>();
        var autorouteEntries = app.ApplicationServices.GetService<IAutorouteEntries>();
        var logger = app.ApplicationServices.GetService<ILogger<MaintenanceRedirectMiddleware>>();

        return app.UseMiddleware<MaintenanceRedirectMiddleware>(siteService, adminOptions, userOptions, autorouteEntries, logger);
    }
}
