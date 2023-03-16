using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.ContentManagement.Routing;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCoreContrib.System.Services;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMaintenanceRedirect(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        var siteService = app.ApplicationServices.GetService<ISiteService>();
        var adminOptions = app.ApplicationServices.GetService<IOptions<AdminOptions>>();
        var userOptions = app.ApplicationServices.GetService<IOptions<UserOptions>>();
        var autorouteEntries = app.ApplicationServices.GetService<IAutorouteEntries>();
        var logger = app.ApplicationServices.GetService<ILogger<MaintenanceRedirectMiddleware>>();

        return app.UseMiddleware<MaintenanceRedirectMiddleware>(siteService, adminOptions, userOptions, autorouteEntries, logger);
    }
}
