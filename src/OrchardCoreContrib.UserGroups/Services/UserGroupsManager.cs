using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using OrchardCore.Documents;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services;

public class UserGroupsManager(IDocumentManager<UserGroupDocument> documentManager, IStringLocalizer<UserGroupsManager> S)
{
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

    public async Task<IEnumerable<UserGroup>> GetUserGroupsAsync()
    {
        var document = await documentManager.GetOrCreateImmutableAsync();

        return document.UserGroups.Values;
    }

    public async Task<UserGroup> FindByNameAsync(string groupName)
    {
        Guard.ArgumentNotNullOrEmpty(groupName, nameof(groupName));

        var document = await documentManager.GetOrCreateImmutableAsync();

        return document.UserGroups.TryGetValue(groupName, out UserGroup userGroup)
            ? userGroup
            : null;
    }
}
