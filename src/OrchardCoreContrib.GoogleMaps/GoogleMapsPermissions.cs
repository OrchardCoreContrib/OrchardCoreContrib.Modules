using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.GoogleMaps;

/// <summary>
/// Represents a permissions that will be applied into GoogleMaps module.
/// </summary>
public class GoogleMapsPermissions
{
    /// <summary>
    /// Gets a permission for managing a Google Maps settings.
    /// </summary>
    public static readonly Permission ManageGoogleMapsSettings = new("ManageGoogleMapsSettings", "Manage Google Maps Settings");
}
