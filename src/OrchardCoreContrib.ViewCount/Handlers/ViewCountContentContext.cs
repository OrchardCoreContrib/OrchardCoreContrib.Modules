using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Handlers;

public class ViewCountContentContext(ContentItem contentItem, int count) : ContentContextBase(contentItem)
{
    public int Count { get; set; } = count;
}
