using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Models;

/// <summary>
/// Represents a content part that tracks the number of times an item has been viewed.
/// </summary>
/// <remarks>Use <see cref="ViewCountPart"/> to associate a view count with content items, enabling features such
/// as analytics or popularity tracking.</remarks>
public class ViewCountPart : ContentPart
{
    /// <summary>
    /// Gets or sets the number of items contained in the collection.
    /// </summary>
    public int Count { get; set; }
}
