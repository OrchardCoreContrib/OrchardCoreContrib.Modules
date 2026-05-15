using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using OrchardCore.Documents;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services;

/// <summary>
/// Represents a manager for user groups.
/// </summary>
/// <param name="documentManager">The <see cref="IDocumentManager{UserGroupDocument}"/>.</param>
/// <param name="S">The <see cref="IStringLocalizer{UserGroupDocument}"/>.</param>
public class UserGroupsManager(IDocumentManager<UserGroupDocument> documentManager, IStringLocalizer<UserGroupsManager> S)
{
    /// <summary>
    /// Creates a new user group.
    /// </summary>
    /// <param name="userGroup">The group to be created.</param>
    public async Task<IdentityResult> CreateAsync(UserGroup userGroup)
    {
        Guard.ArgumentNotNull(userGroup, nameof(userGroup));
        
        var invalidChars = Path.GetInvalidPathChars();
        if (userGroup.Name.ToArray().Intersect(invalidChars).Any())
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = S["Invalid user group name."],
            });
        }

        var document = await documentManager.GetOrCreateMutableAsync();

        if (document.UserGroups.ContainsKey(userGroup.Name))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = S["The user group '{0}' already exists.", userGroup.Name],
            });
        }

        document.UserGroups.Add(userGroup.Name, userGroup);
        
        await documentManager.UpdateAsync(document);

        return IdentityResult.Success;
    }

    /// <summary>
    /// Deletes a user group.
    /// </summary>
    /// <param name="groupName">The name of the group to be deleted.</param>
    public async Task<IdentityResult> DeleteAsync(string groupName)
    {
        Guard.ArgumentNotNullOrEmpty(groupName, nameof(groupName));

        var document = await documentManager.GetOrCreateMutableAsync();

        if (document.UserGroups.Remove(groupName))
        {
            await documentManager.UpdateAsync(document);

            return IdentityResult.Success;
        }
        else
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = S["The user group '{0}' does not exist.", groupName]
            });
        }
    }

    /// <summary>
    /// Updates an existing user group.
    /// </summary>
    /// <param name="userGroup">The group to be updated.</param>
    public async Task<IdentityResult> UpdateAsync(UserGroup userGroup)
    {
        Guard.ArgumentNotNull(userGroup, nameof(userGroup));

        var document = await documentManager.GetOrCreateMutableAsync();

        if (document.UserGroups.ContainsKey(userGroup.Name))
        {
            document.UserGroups[userGroup.Name] = userGroup;

            await documentManager.UpdateAsync(document);

            return IdentityResult.Success;
        }
        else
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = S["The user group '{0}' does not exist.", userGroup.Name]
            });
        }
    }

    /// <summary>
    /// Retrieves all user groups.
    /// </summary>
    public async Task<IEnumerable<UserGroup>> GetUserGroupsAsync()
    {
        var document = await documentManager.GetOrCreateImmutableAsync();

        return document.UserGroups.Values;
    }

    /// <summary>
    /// Finds a user group by its name.
    /// </summary>
    /// <param name="groupName">The group name that's used for retrieval.</param>
    public async Task<UserGroup> FindByNameAsync(string groupName)
    {
        Guard.ArgumentNotNullOrEmpty(groupName, nameof(groupName));

        var document = await documentManager.GetOrCreateImmutableAsync();

        return document.UserGroups.TryGetValue(groupName, out UserGroup userGroup)
            ? userGroup
            : null;
    }
}
