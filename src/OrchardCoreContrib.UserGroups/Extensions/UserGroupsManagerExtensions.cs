using Microsoft.AspNetCore.Identity;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services;

public static class UserGroupsManagerExtensions
{
    public static async Task<IEnumerable<string>> GetUserGroupNamesAsync(this UserGroupsManager userGroupsManager)
    {
        Guard.ArgumentNotNull(userGroupsManager, nameof(userGroupsManager));
        
        var userGroups = await userGroupsManager.GetUserGroupsAsync();

        return userGroups.Select(group => group.Name);
    }

    public static async Task<IdentityResult> CreateAsync(this UserGroupsManager userGroupsManager, string name, string description)
    {
        Guard.ArgumentNotNull(userGroupsManager, nameof(userGroupsManager));
        Guard.ArgumentNotNullOrEmpty(name, nameof(name));

        return await userGroupsManager.CreateAsync(new UserGroup
        {
            Name = name,
            Description = description
        });
    }
}
