using System.Net;

namespace OrchardCoreContrib.Ban.Services;

public interface IIPBanService
{
    Task<bool> IsBannedAsync(IPAddress ipAddress);
}
