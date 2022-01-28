using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Environment.Shell;
using OrchardCore.Users;
using OrchardCoreContrib.ContentPreview;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides an extension methods for <see cref="IApplicationBuilder"/> to enable page preview.
    /// </summary>
    public static class PagePreviewOrchardCoreExtensions
    {
        /// <summary>
        /// Uses the page preview middleware.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        public static IApplicationBuilder UsePagePreview(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var adminOptions = app.ApplicationServices.GetService<IOptions<AdminOptions>>();
            var userOptions = app.ApplicationServices.GetService<IOptions<UserOptions>>();
            var shellFeaturesManager = app.ApplicationServices.CreateScope().ServiceProvider.GetService<IShellFeaturesManager>();
            var shellSettings = app.ApplicationServices.GetService<ShellSettings>();

            app.UseMiddleware<PagePreviewMiddleware>(adminOptions, userOptions, shellFeaturesManager, shellSettings);

            return app;
        }
    }
}
