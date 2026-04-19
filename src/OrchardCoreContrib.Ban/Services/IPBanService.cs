using OrchardCore.Settings;
using OrchardCoreContrib.Ban.Models;
using System.Net;

namespace OrchardCoreContrib.Ban.Services;

public class IPBanService(ISiteService siteService) : IIPBanService
{
    public async Task<bool> IsBannedAsync(IPAddress ipAddress)
    {
        var ipBanSettings = await siteService.GetSettingsAsync<BanSettings>();

        if (ipBanSettings?.BannedIPs is null)
        {
            return false;
        }

        return ipBanSettings.BannedIPs.Any(ip => ip == ipAddress.ToString());
    }
}
