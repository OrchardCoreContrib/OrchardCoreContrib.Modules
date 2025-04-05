using OrchardCore.Security.Permissions;
using OrchardCore;

namespace OrchardCoreContrib.UserGroups;

public sealed class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _permissions = [UserGroupsPermissions.ManageUserGroups];

    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(_permissions);

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
    [
        new PermissionStereotype
        {
            Name = OrchardCoreConstants.Roles.Administrator,
            Permissions = _permissions,
        },
    ];
}
