using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.CloudflareTurnstile;

public static class TurnstilePermissions
{
    public static readonly Permission ManageTurnstileSettings = new("ManageTurnstileSettings", "Manage Turnstile Settings");
}
