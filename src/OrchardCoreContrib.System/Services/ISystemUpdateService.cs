using OrchardCoreContrib.System.Models;

namespace OrchardCoreContrib.System.Services;

public interface ISystemUpdateService
{
    Task<IEnumerable<SystemUpdate>> GetUpdatesAsync();
}
