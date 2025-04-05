using OrchardCoreContrib.Infrastructure;
using System.Text.Json.Nodes;

namespace OrchardCore.Users.Models;

public static class UserExtensions
{
    private const string UserGroupsPropertyName = "UserGroups";

    public static string[] GetUserGroups(this User user)
    {
        Guard.ArgumentNotNull(user, nameof(user));

        return user.Properties.TryGetPropertyValue(UserGroupsPropertyName, out var userGroupsJson)
            ? userGroupsJson.ToObject<string[]>()
            : [];
    }

    public static void SetUserGroups(this User user, string[] groups)
    {
        Guard.ArgumentNotNull(user, nameof(user));
        Guard.ArgumentNotNull(groups, nameof(groups));

        user.Properties[UserGroupsPropertyName] = JNode.FromObject(groups);
    }
}
