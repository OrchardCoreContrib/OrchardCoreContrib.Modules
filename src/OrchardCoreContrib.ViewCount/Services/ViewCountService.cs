using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.Modules;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.ViewCount.Handlers;
using OrchardCoreContrib.ViewCount.Models;

namespace OrchardCoreContrib.ViewCount.Services;

public class ViewCountService(
    IContentManager contentManager,
    IEnumerable<IViewCountContentHandler> handlers,
    ILogger<ViewCountService> logger) : IViewCountService
{
    public int GetViewsCount(ContentItem contentItem)
    {
        Guard.ArgumentNotNull(contentItem, nameof(contentItem));

        var viewCountPart = contentItem.As<ViewCountPart>();
        
        return viewCountPart?.Count ?? 0;
    }

    public async Task ViewAsync(ContentItem contentItem)
    {
        Guard.ArgumentNotNull(contentItem, nameof(contentItem));

        var viewCountPart = contentItem.As<ViewCountPart>()
            ?? throw new InvalidOperationException($"The content item doesn't have a `{nameof(ViewCountPart)}`.");
        var count = viewCountPart.Count;
        var context = new ViewCountContentContext(contentItem, count);

        await handlers.InvokeAsync((handler, context) => handler.ViewingAsync(context), context, logger);

        contentItem.Content.ViewCountPart.Count = ++count;

        await contentManager.UpdateAsync(contentItem);

        context = new ViewCountContentContext(contentItem, count);

        await handlers.InvokeAsync((handler, context) => handler.ViewedAsync(context), context, logger);
    }
}
