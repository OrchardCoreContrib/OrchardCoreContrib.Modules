using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ViewCount.Services;

public interface IViewCountService
{
    int GetViewsCount(ContentItem contentItem);

    Task ViewAsync(ContentItem contentItem);
}
