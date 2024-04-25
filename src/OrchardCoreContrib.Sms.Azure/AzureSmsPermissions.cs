using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Sms.Azure;

public class AzureSmsPermissions
{
    public static readonly Permission ManageSettings = new("ManageSettings", "Manage Azure SMS Settings");
}
