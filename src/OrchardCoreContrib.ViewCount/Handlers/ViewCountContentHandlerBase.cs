namespace OrchardCoreContrib.ViewCount.Handlers;

/// <summary>
/// Provides a base implementation for handling view count events for content items.
/// </summary>
public abstract class ViewCountContentHandlerBase : IViewCountContentHandler
{
    /// <inheritdoc/>
    public virtual Task ViewingAsync(ViewCountContentContext context) => Task.CompletedTask;

    /// <inheritdoc/>
    public virtual Task ViewedAsync(ViewCountContentContext context) => Task.CompletedTask;
}
