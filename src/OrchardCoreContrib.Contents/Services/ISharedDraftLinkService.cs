using OrchardCore.ContentManagement;
using OrchardCoreContrib.Contents.Models;

namespace OrchardCoreContrib.Contents.Services;

public interface ISharedDraftLinkService
{
    Task<string> GenerateLinkAsync(string contentItemId);

    string GetGeneratedLink(string token);

    Task<ContentItem> GetDraftContentAsync(string token);

    Task<int> CleanupExpiredLinksAsync();

    Task<bool> RevokeLinkAsync(string contentItemId);

    Task<SharedDraftLink> GetActiveLinkAsync(string contentItemId);
}
