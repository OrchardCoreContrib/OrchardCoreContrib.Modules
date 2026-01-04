using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Yahoo;

/// <summary>
/// Provides predefined permissions related to Yahoo settings.
/// </summary>
public class YahooPermissions
{
    /// <summary>
    /// Gets a permission for managing a Yahoo settings.
    /// </summary>
    public static readonly Permission ManageYahooSettings = new("ManageYahooSettings", "Manage Yahoo Settings");
}
