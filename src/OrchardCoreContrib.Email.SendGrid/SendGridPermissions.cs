using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.SendGrid;

/// <summary>
/// Provides predefined permissions related to SendGrid settings.
/// </summary>
public class SendGridPermissions
{
    /// <summary>
    /// Gets a permission for managing a SendGrid settings.
    /// </summary>
    public static readonly Permission ManageSendGridSettings = new("ManageSendGridSettings", "Manage SendGrid Settings");
}
