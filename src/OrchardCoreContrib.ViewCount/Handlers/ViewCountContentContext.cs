using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Handlers;

/// <summary>
/// Represents a content context that includes the view count of a content item.
/// </summary>
/// <remarks>
/// Use <see cref="ViewCountContentContext"/> to associate a content item with its recorded number of views.
/// </remarks>
public class ViewCountContentContext(ContentItem contentItem, int count) : ContentContextBase(contentItem)
{
    /// <summary>
    /// Gets or sets the number of views recorded for the content item.
    /// </summary>
    public int Count { get; set; } = count;
}
