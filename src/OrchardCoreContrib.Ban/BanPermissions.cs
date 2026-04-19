using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Ban;

public static class BanPermissions
{
    public static readonly Permission ManageBanSettings = new("ManageBanSettings", "Manage Ban Settings");
}
