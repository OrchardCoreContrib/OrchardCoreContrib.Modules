using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Layout;

namespace OrchardCoreContrib.Gdpr;

/// <summary>
/// Represents a filter that inject a cookie consent shape into the layout.
/// </summary>
public class CookieConsentFilter(
    IOptions<AdminOptions> adminOptions,
    ILayoutAccessor layoutAccessor,
    IShapeFactory shapeFactory) : IAsyncResultFilter
{
    private readonly string _adminUrlPrefix = adminOptions.Value.AdminUrlPrefix;

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
            var layout = await layoutAccessor.GetLayoutAsync();
            
            var contentZone = layout.Zones["Content"];

            await contentZone.AddAsync((object)await shapeFactory.New.CookieConsent());
        }

        await next();
    }
}
