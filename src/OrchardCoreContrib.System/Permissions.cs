using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents a permissions that will be applied into System module.
/// </summary>
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        SystemPermissions.ManageSystemSettings,
    ];

    /// <inheritdoc/>
    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(_allPermissions);

    /// <inheritdoc/>
    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
    [
        new PermissionStereotype
        {
            Name = "Administrator",
            Permissions = _allPermissions
        },
    ];
}
