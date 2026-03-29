using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ContentPermissions.Services;

/// <summary>
/// Represents a contract for a service that provides content permissions.
/// </summary>
public interface IContentPermissionsService
{
    /// <summary>
    /// Authorizes the specified content item.
    /// </summary>
    /// <param name="contentItem">The content item to be checked.</param>
    Task<bool> AuthorizeAsync(ContentItem contentItem);
}
