using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.UserGroups.Services;

public static class UserGroupsManagerExtensions
{
    public static async Task<IEnumerable<string>> GetUserGroupNamesAsync(this UserGroupsManager userGroupsManager)
    {
        Guard.ArgumentNotNull(userGroupsManager, nameof(userGroupsManager));
        
        var userGroups = await userGroupsManager.GetUserGroupsAsync();

        return userGroups.Select(group => group.Name);
    }
}
