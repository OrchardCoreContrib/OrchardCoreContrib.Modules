using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Environment.Shell;
using OrchardCore.Users;

namespace OrchardCoreContrib.ContentPreview;

/// <summary>
/// Represents a middleware for a page preview.
/// </summary>
/// <remarks>
/// Craates a new instance of <see cref="PagePreviewMiddleware"/>.
/// </remarks>
/// <param name="next">The <see cref="RequestDelegate"/>.</param>
/// <param name="adminOptions">The <see cref="IOptions{AdminOptions}"/>.</param>
/// <param name="userOptions">The <see cref="IOptions{UserOptions}"/>.</param>
/// <param name="shellFeaturesManager">The <see cref="IShellFeaturesManager"/>.</param>
public class PagePreviewMiddleware(
    RequestDelegate next,
    IOptions<AdminOptions> adminOptions,
    IOptions<UserOptions> userOptions,
    IShellFeaturesManager shellFeaturesManager)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    private readonly UserOptions _userOptions = userOptions.Value;

    /// <summary>
    /// Invokes the logic of the middleware.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        // Skip if the user is not authenticated
        if (!context.User.Identity.IsAuthenticated)
        {
            await next(context);

            return;
        }

        // Skip if the current request for a login page
        if (path.StartsWith($"/{_userOptions.LoginPath}", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);

            return;
        }

        // Skip if the current request for an admin page
        if (path.StartsWith($"/{_adminOptions.AdminUrlPrefix}", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);

            return;
        }

        var enabledFeatures = await shellFeaturesManager.GetEnabledFeaturesAsync();

        if (!enabledFeatures.Any(f => f.Id == Constants.PagePreviewBarFeatureId))
        {
            await next(context);

            return;
        }

        var isPreview = context.Request.Query.ContainsKey(Constants.PreviewSlug);

        if (!path.Contains(Constants.PreviewSlug) && !isPreview)
        {
            var url = string.Concat(context.Request.PathBase.Value, $"/{Constants.PreviewSlug}", context.Request.Path.Value);
            
            context.Response.Redirect(url);
        }

        await next(context);
    }
}
