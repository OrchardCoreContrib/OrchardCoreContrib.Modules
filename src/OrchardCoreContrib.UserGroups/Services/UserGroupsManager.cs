using OrchardCore.Documents;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services;

public class UserGroupsManager(IDocumentManager<UserGroupDocument> documentManager)
{
    public async Task CreateAsync(UserGroup userGroup)
    {
        Guard.ArgumentNotNull(userGroup, nameof(userGroup));

        var document = await documentManager.GetOrCreateMutableAsync();

        document.UserGroups.Add(userGroup.Name, userGroup);
        
        await documentManager.UpdateAsync(document);
    }

    public async Task DeleteAsync(string groupName)
    {
        Guard.ArgumentNotNullOrEmpty(groupName, nameof(groupName));

        var document = await documentManager.GetOrCreateMutableAsync();
        
        document.UserGroups.Remove(groupName);

        await documentManager.UpdateAsync(document);
    }

    public async Task UpdateAsync(UserGroup userGroup)
    {
        Guard.ArgumentNotNull(userGroup, nameof(userGroup));

        var document = await documentManager.GetOrCreateMutableAsync();

        document.UserGroups[userGroup.Name] = userGroup;

        await documentManager.UpdateAsync(document);
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
