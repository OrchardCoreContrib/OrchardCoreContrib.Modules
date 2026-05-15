using OrchardCoreContrib.System.Models;

namespace OrchardCoreContrib.System.Services;

/// <summary>
/// Provides functionality to retrieve available system updates asynchronously.
/// </summary>
public interface ISystemUpdateService
{
    /// <summary>
    /// Retrieves a collection of available system updates.
    /// </summary>
    Task<IEnumerable<SystemUpdate>> GetUpdatesAsync();
}
