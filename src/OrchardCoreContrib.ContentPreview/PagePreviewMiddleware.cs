using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Environment.Shell;
using OrchardCore.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.ContentPreview
{
    /// <summary>
    /// Represents a middleware for a page preview.
    /// </summary>
    public class PagePreviewMiddleware
    {
        private readonly AdminOptions _adminOptions;
        private readonly UserOptions _userOptions;
        private readonly IShellFeaturesManager _shellFeaturesManager;
        private readonly ShellSettings _shellSettings;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Craates a new instance of <see cref="PagePreviewMiddleware"/>.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/>.</param>
        /// <param name="adminOptions">The <see cref="IOptions{AdminOptions}"/>.</param>
        /// <param name="shellFeaturesManager">The <see cref="IShellFeaturesManager"/>.</param>
        /// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
        public PagePreviewMiddleware(
            RequestDelegate next,
            IOptions<AdminOptions> adminOptions,
            IOptions<UserOptions> userOptions,
            IShellFeaturesManager shellFeaturesManager,
            ShellSettings shellSettings)
        {
            _next = next;
            _adminOptions = adminOptions.Value;
            _userOptions = userOptions.Value;
            _shellFeaturesManager = shellFeaturesManager;
            _shellSettings = shellSettings;
        }

        /// <summary>
        /// Invokes the logic of the middleware.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        public Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;

            // Skip if the user is not authenticated
            if (!context.User.Identity.IsAuthenticated)
            {
                return _next(context);
            }

            // Skip if the current request for a login page
            if (path.StartsWith($"/{_userOptions.LoginPath}", StringComparison.OrdinalIgnoreCase))
            {
                return _next(context);
            }

            // Skip if the current request for an admin page
            if (path.StartsWith($"/{_adminOptions.AdminUrlPrefix}", StringComparison.OrdinalIgnoreCase))
            {
                return _next(context);
            }

            var featureEnabled = _shellFeaturesManager
                .GetEnabledFeaturesAsync().Result
                .Any(f => f.Id == "OrchardCoreContrib.ContentPreview.PagePreviewBar");

            if (!featureEnabled)
            {
                return _next(context);
            }

            var isPreview = context.Request.Query.ContainsKey("preview");

            if (!path.Contains("preview") && !isPreview)
            {
                var url = _shellSettings.Name == "Default"
                    ? "preview" + path
                    : $"/{_shellSettings.RequestUrlPrefix}/preview{path}";
                
                context.Response.Redirect(url);
            }

            return _next(context);
        }
    }
}
