using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.UserGroups;

public static class UserGroupsPermissions
{
    public static readonly Permission ManageUserGroups = new("ManageUserGroups", "Managing User Groups");
}
