using OrchardCore.Modules;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Users;

/// <summary>
/// Represents a permissions that will be applied into users module.
/// </summary>
[Feature("OrchardCoreContrib.Users.Impersonation")]
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        UsersPermissions.ManageImpersonationSettings,
    ];

    /// <inheritdoc/>
    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(_allPermissions);

    /// <inheritdoc/>
    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return
        [
            new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = _allPermissions
            },
        ];
    }
}
