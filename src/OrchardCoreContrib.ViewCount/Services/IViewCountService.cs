using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Services;

/// <summary>
/// Defines methods for retrieving and recording view counts for content items.
/// </summary>
/// <remarks>Implementations of this interface provide functionality to track how many times a content item has
/// been viewed and to record new views. This is typically used for analytics, popularity metrics, or display purposes
/// in content management scenarios.</remarks>
public interface IViewCountService
{
    /// <summary>
    /// Returns the total number of views recorded for the specified content item.
    /// </summary>
    /// <param name="contentItem">The content item for which to retrieve the view count. Cannot be <c>null</c>.</param>
    /// <returns>The number of times the specified content item has been viewed. Returns 0 if no views have been recorded.</returns>
    int GetViewsCount(ContentItem contentItem);

    /// <summary>
    /// Increments the views number for the specified content item asynchronously in the current view context.
    /// </summary>
    /// <param name="contentItem">The content item to be displayed. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous display operation.</returns>
    Task ViewAsync(ContentItem contentItem);
}
