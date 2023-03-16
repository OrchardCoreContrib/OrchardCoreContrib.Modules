using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents a permissions that will be applied into System module.
/// </summary>
public class Permissions : IPermissionProvider
{
    /// <summary>
    /// Gets a permission for managing a System settings.
    /// </summary>
    public static readonly Permission ManageSystemSettings = new("ManageSystemSettings", "Manage System Settings");

    /// <inheritdoc/>
    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(new[] { ManageSystemSettings }.AsEnumerable());

    /// <inheritdoc/>
    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() => new[]
    {
        new PermissionStereotype
        {
            Name = "Administrator",
            Permissions = new[] { ManageSystemSettings }
        },
    };
}
