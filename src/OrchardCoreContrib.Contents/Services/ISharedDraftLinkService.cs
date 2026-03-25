using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.Contents.Services;

public interface ISharedDraftLinkService
{
    Task<string> GenerateLinkAsync(ContentItem contentItem);

    Task<ContentItem> GetDraftContentAsync(string token);

    Task<int> CleanupExpiredLinksAsync();
}
