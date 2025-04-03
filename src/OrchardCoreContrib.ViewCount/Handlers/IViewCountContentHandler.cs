namespace OrchardCoreContrib.ViewCount.Handlers;

public interface IViewCountContentHandler
{
    Task ViewingAsync(ViewCountContentContext context);

    Task ViewedAsync(ViewCountContentContext context);
}
