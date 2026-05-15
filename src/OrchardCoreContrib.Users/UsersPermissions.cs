using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Users;

/// <summary>
/// Provides predefined permissions related to Users module.
/// </summary>
public class UsersPermissions
{
    /// <summary>
    /// Gets a permission for managing a impersonation settings.
    /// </summary>
    public static readonly Permission ManageImpersonationSettings = new("ManageImpersonationSettings", "Manage Impersonation Settings", isSecurityCritical: true);
}
