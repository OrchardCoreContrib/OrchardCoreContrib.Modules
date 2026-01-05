using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Handlers;

/// <summary>
/// Represents a content context that includes a count of items within a collection.
/// </summary>
/// <remarks>Use <see cref="ViewCountContentContext"/> to associate a specific item of content with a count value,
/// such as the number of times the content has been viewed or the number of related items.</remarks>
public class ViewCountContentContext(ContentItem contentItem, int count) : ContentContextBase(contentItem)
{
    /// <summary>
    /// Gets or sets the number of items contained in the collection.
    /// </summary>
    public int Count { get; set; } = count;
}
