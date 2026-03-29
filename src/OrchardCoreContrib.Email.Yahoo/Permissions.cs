using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Yahoo;

/// <summary>
/// Represents a permissions that will be applied into Yahoo mailing module.
/// </summary>
public class Permissions : IPermissionProvider
{
    private readonly IEnumerable<Permission> _allPermissions =
    [
        YahooPermissions.ManageYahooSettings,
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
                Permissions = _allPermissions }
            ];
    }
}
