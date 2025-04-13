using Microsoft.AspNetCore.Identity;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services;

/// <summary>
/// Provides extension methods for the <see cref="UserGroupsManager"/>.
/// </summary>
public static class UserGroupsManagerExtensions
{
    /// <summary>
    /// Gets the user group names.
    /// </summary>
    /// <param name="userGroupsManager">The <see cref="UserGroupsManager"/>.</param>
    public static async Task<IEnumerable<string>> GetUserGroupNamesAsync(this UserGroupsManager userGroupsManager)
    {
        Guard.ArgumentNotNull(userGroupsManager, nameof(userGroupsManager));
        
        var userGroups = await userGroupsManager.GetUserGroupsAsync();

        return userGroups.Select(group => group.Name);
    }

    /// <summary>
    /// Creates a new user group with the specified name and description.
    /// </summary>
    /// <param name="userGroupsManager">The <see cref="UserGroupsManager"/>.</param>
    /// <param name="name">The group name.</param>
    /// <param name="description">The group description.</param>
    /// <returns></returns>
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
