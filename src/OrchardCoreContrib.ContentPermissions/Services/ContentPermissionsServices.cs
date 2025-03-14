using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCoreContrib.ContentPermissions.Models;
using OrchardCoreContrib.Infrastructure;
using static OrchardCore.OrchardCoreConstants;

namespace OrchardCoreContrib.ContentPermissions.Services;

/// <summary>
/// Represents a service that provides content permissions.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="ContentPermissionsServices"/>
/// </remarks>
/// <param name="contentDefinitionManager">The <see cref="IContentDefinitionManager"/>.</param>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
public class ContentPermissionsServices(IContentDefinitionManager contentDefinitionManager, IHttpContextAccessor httpContextAccessor) : IContentPermissionsService
{
    /// <inheritdoc/>
    public async Task<bool> AuthorizeAsync(ContentItem contentItem)
    {
        Guard.ArgumentNotNull(contentItem, nameof(contentItem));

        if (contentItem.Has<ContentPermissionsPart>())
        {
            var contentPermissionsPartSettings = await GetContentPermissionsPartSettingsAsync(contentItem);

            var contentPermissionsPart = contentItem.As<ContentPermissionsPart>();

            if (contentPermissionsPartSettings.EnableRoles == false && contentPermissionsPartSettings.EnableUsers == false)
            {
                return true;
            }

            if (contentPermissionsPartSettings.EnableRoles && IsInRoles(contentPermissionsPart.Roles))
            {
                return true;
            }

            if (contentPermissionsPartSettings.EnableUsers && IsInUsers(contentPermissionsPart.Users))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsInRoles(string[] roles)
    {
        if (roles is null || roles?.Length == 0)
        {
            return true;
        }

        if (roles.Contains(Roles.Anonymous))
        {
            return true;
        }

        if (roles.Contains(Roles.Authenticated) && (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false))
        {
            return true;
        }

        foreach (var role in roles)
        {
            if (httpContextAccessor.HttpContext.User.IsInRole(role))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsInUsers(string[] users)
    {
        if (users is null || users?.Length == 0)
        {
            return false;
        }

        if (users.Contains(httpContextAccessor.HttpContext.User.Identity.Name))
        {
            return true;
        }

        return false;
    }

    private async Task<ContentPermissionsPartSettings> GetContentPermissionsPartSettingsAsync(ContentItem contentItem)
    {
        var contentTypeDefinition = await contentDefinitionManager.GetTypeDefinitionAsync(contentItem.ContentType);

        var contentTypePartDefinition = contentTypeDefinition.Parts
            .Single(part => part.PartDefinition.Name == Constants.ContentPermissionsPartName);

        return contentTypePartDefinition.GetSettings<ContentPermissionsPartSettings>();
    }
}
