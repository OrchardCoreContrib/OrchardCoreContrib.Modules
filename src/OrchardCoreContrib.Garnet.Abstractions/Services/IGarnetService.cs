using Garnet.client;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Garnet.Services;

/// <summary>
/// Represents a contract for Garnet service.
/// </summary>
public interface IGarnetService : IModularTenantEvents
{
    /// <summary>
    /// Connects to Garnet server.
    /// </summary>
    Task ConnectAsync();

    /// <summary>
    /// Gets the Garnet client.
    /// </summary>
    GarnetClient Client { get; }

    /// <summary>
    /// Gets the Garnet instance prefix.
    /// </summary>
    string InstancePrefix { get; }
}
