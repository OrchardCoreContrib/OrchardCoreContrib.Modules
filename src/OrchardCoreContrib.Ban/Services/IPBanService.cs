using OrchardCore.Settings;
using OrchardCore.ContentManagement.Routing;
using OrchardCoreContrib.Ban.Models;
using System.Net;

namespace OrchardCoreContrib.Ban.Services;

public class IPBanService(ISiteService siteService, IAutorouteEntries autorouteEntries) : IIPBanService
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

    public async Task<string> GetRedirectUrlAsync()
    {
        var settings = await siteService.GetSettingsAsync<BanSettings>();
        var redirectUrl = settings.RedirectUrl?.Trim();

        if (string.IsNullOrEmpty(redirectUrl))
        {
            return null;
        }

        if (!redirectUrl.StartsWith('/'))
        {
            redirectUrl = "/" + redirectUrl;
        }

        // Validate the URL exists in the CMS
        var (found, _) = await autorouteEntries.TryGetEntryByPathAsync(redirectUrl);
        if (found)
        {
            return redirectUrl;
        }

        return null;
    }
}
