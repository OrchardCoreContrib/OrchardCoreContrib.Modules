namespace OrchardCoreContrib.ViewCount.Handlers;

/// <summary>
/// Defines methods for handling view count events for content.
/// </summary>
public interface IViewCountContentHandler
{
    /// <summary>
    /// Occurs before the content is viewed.
    /// </summary>
    /// <param name="context">The <see cref="ViewCountContentContext"/> that identifies the content to record the view for. Cannot be
    /// <c>null</c>.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ViewingAsync(ViewCountContentContext context);

    /// <summary>
    /// Occurs after the content is viewed.
    /// </summary>
    /// <param name="context">The context containing information about the content whose view count should be updated. Must not be
    /// <c>null</c>.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ViewedAsync(ViewCountContentContext context);
}
