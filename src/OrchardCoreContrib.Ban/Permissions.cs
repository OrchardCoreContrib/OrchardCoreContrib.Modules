using OrchardCore;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Ban;

public class Permissions : IPermissionProvider
{
    private static readonly IEnumerable<Permission> _allPermissions =
    [
        BanPermissions.ManageBanSettings
    ];

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
    [
        new PermissionStereotype
        {
            Name = OrchardCoreConstants.Roles.Administrator,
            Permissions = _allPermissions
        }
    ];

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
        => Task.FromResult(_allPermissions);
}
