using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Gdpr;

/// <summary>
/// Represents a permissions that will be applied into GDPR module.
/// </summary>
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        GdprPermissions.ManageGdprSettings,
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
