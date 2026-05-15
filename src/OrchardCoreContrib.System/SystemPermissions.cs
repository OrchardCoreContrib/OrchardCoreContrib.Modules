using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.System;

/// <summary>
/// Provides predefined permissions related to system-level operations, such as managing system settings.
/// </summary>
public static class SystemPermissions
{
    /// <summary>
    /// Defines a permission for managing a System settings.
    /// </summary>
    public static readonly Permission ManageSystemSettings = new("ManageSystemSettings", "Manage System Settings");
}
