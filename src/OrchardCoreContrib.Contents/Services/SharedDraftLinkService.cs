using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCoreContrib.Contents.Indexes;
using OrchardCoreContrib.Contents.Models;
using YesSql;

namespace OrchardCoreContrib.Contents.Services;

public class SharedDraftLinkService(
    IContentManager contentManager,
    IHttpContextAccessor httpContextAccessor,
    YesSql.ISession session) : ISharedDraftLinkService
{
    public async Task<string> GenerateLinkAsync(ContentItem contentItem)
    {
        ArgumentNullException.ThrowIfNull(contentItem);

        var existingLink = await session.Query<SharedDraftLink, SharedDraftLinkIndex>()
            .Where(l => l.ContentItemId == contentItem.ContentItemId && l.ExpirationUtc > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if (existingLink is not null)
        {
            return $"/share/{existingLink.Token}";
        }

        var token = Guid.NewGuid().ToString("N");
        var link = new SharedDraftLink
        {
            ContentItemId = contentItem.ContentItemId,
            Token = token,
            ExpirationUtc = DateTime.UtcNow.AddDays(30),
            CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name,
            CreatedUtc = DateTime.UtcNow
        };

        await session.SaveAsync(link);

        return $"/share/{token}";
    }

    public async Task<ContentItem> GetDraftContentAsync(string token)
    {
        ArgumentException.ThrowIfNullOrEmpty(token);

        var link = await session.Query<SharedDraftLink, SharedDraftLinkIndex>()
            .Where(l => l.Token == token && l.ExpirationUtc > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        return link is null
            ? null
            : await contentManager.GetAsync(link.ContentItemId, VersionOptions.Draft);
    }

    public async Task<int> CleanupExpiredLinksAsync()
    {
        var expiredLinks = await session.Query<SharedDraftLink, SharedDraftLinkIndex>()
            .Where(l => l.ExpirationUtc < DateTime.UtcNow)
            .ListAsync();

        foreach (var link in expiredLinks)
        {
            session.Delete(link);
        }

        return expiredLinks.Count();
    }

    public async Task<bool> RevokeLinkAsync(string contentItemId)
    {
        var link = await session.Query<SharedDraftLink, SharedDraftLinkIndex>()
            .Where(l => l.ContentItemId == contentItemId && l.ExpirationUtc > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if (link is null)
        {
            return false;
        }

        // Enforce expiration by setting the ExpirationUtc to the current time, so the link will be considered expired.
        link.ExpirationUtc = DateTime.UtcNow;

        await session.SaveAsync(link);

        return true;
    }
}
