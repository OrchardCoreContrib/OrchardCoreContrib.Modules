using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Sms.Azure;

public static class AzureSmsPermissions
{
    public static readonly Permission ManageAzureSmsSettings = new("ManageAzureSmsSettings", "Manage Azure SMS Settings");
}
