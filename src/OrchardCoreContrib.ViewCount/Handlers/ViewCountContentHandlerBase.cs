namespace OrchardCoreContrib.ViewCount.Handlers;

public abstract class ViewCountContentHandlerBase : IViewCountContentHandler
{
    public virtual Task ViewingAsync(ViewCountContentContext context) => Task.CompletedTask;

    public virtual Task ViewedAsync(ViewCountContentContext context) => Task.CompletedTask;
}
