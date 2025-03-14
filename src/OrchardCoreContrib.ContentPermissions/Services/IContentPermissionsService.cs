using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ContentPermissions.Services;

public interface IContentPermissionsService
{
    Task<bool> AuthorizeAsync(ContentItem contentItem);
}
