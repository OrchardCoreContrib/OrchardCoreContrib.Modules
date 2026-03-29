using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Hotmail;

/// <summary>
/// Provides predefined permissions related to Hotmail settings.
/// </summary>
public class HotmailPermissions
{
    /// <summary>
    /// Gets a permission for managing a Hotmail settings.
    /// </summary>
    public static readonly Permission ManageHotmailSettings = new("ManageHotmailSettings", "Manage Hotmail Settings");
}
