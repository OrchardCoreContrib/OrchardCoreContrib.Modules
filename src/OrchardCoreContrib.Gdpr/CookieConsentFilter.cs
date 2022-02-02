using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Gdpr
{
    /// <summary>
    /// Represents a filter that inject a cookie consent shape into the layout.
    /// </summary>
    public class CookieConsentFilter : IAsyncResultFilter
    {
        private readonly string _adminUrlPrefix;
        private readonly ILayoutAccessor _layoutAccessor;
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="CookieConsentFilter"/>.
        /// </summary>
        /// <param name="adminOptions">The <see cref="IOptions{AdminOptions}"/>.</param>
        /// <param name="layoutAccessor">The <see cref="ILayoutAccessor"/>,</param>
        /// <param name="shapeFactory">The <see cref="IShapeFactory"/>.</param>
        public CookieConsentFilter(
            IOptions<AdminOptions> adminOptions,
            ILayoutAccessor layoutAccessor,
            IShapeFactory shapeFactory)
        {
            _adminUrlPrefix = adminOptions.Value.AdminUrlPrefix;
            _layoutAccessor = layoutAccessor;
            _shapeFactory = shapeFactory;
        }

        /// <inheritdoc/>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.HttpContext.Request.Path.Value.Contains(_adminUrlPrefix, StringComparison.OrdinalIgnoreCase))
            {
                await next();

                return;
            }

            var consentFeature = context.HttpContext.Features.Get<ITrackingConsentFeature>();
            if (!consentFeature?.CanTrack ?? false)
            {
                dynamic layout = await _layoutAccessor.GetLayoutAsync();
                var contentZone = layout.Zones["Content"];
                contentZone.Add(await _shapeFactory.New.CookieConsent());
            }

            await next();
        }
    }
}
