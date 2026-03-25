using OrchardCore;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Contents;

public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions = [ContentsPermissions.ShareDraftContent];

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
    [
        new PermissionStereotype
        {
            Name = OrchardCoreConstants.Roles.Administrator,
            Permissions = _allPermissions
        },
        new PermissionStereotype
        {
            Name = OrchardCoreConstants.Roles.Editor,
            Permissions = _allPermissions
        }
    ];

    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(_allPermissions);
}
