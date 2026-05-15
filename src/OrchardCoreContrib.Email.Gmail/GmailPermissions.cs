using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Gmail;

/// <summary>
/// Provides predefined permissions related to GMail settings.
/// </summary>
public static class GmailPermissions
{
    /// <summary>
    /// Gets a permission for managing a Gmail settings.
    /// </summary>
    public static readonly Permission ManageGmailSettings = new("ManageGmailSettings", "Manage Gmail Settings");
}
