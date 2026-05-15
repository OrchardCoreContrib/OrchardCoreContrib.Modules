using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Gmail;

/// <summary>
/// Represents a permissions that will be applied into Gmail mailing module.
/// </summary>
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        GmailPermissions.ManageGmailSettings,
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
