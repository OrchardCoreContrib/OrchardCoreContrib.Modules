using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Gdpr;

/// <summary>
/// Provides predefined permissions related to the GDPR module.
/// </summary>
public class GdprPermissions
{
    /// <summary>
    /// Gets a permission for managing a GDPR settings.
    /// </summary>
    public static readonly Permission ManageGdprSettings = new("ManageGdprSettings", "Manage GDPR Settings");
}
