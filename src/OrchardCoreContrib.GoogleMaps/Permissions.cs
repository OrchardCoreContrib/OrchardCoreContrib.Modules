using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.GoogleMaps;

/// <summary>
/// Represents a permissions that will be applied into GoogleMaps module.
/// </summary>
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        GoogleMapsPermissions.ManageGoogleMapsSettings,
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
