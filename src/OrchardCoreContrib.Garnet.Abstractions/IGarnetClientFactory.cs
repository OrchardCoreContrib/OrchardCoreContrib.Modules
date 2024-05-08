using Garnet.client;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a contract for creating a <see cref="GarnetClient"/>.
/// </summary>
public interface IGarnetClientFactory
{
    /// <summary>
    /// Creates a Garnet client for a given options.
    /// </summary>
    /// <param name="options">The <see cref="GarnetOptions"/>.</param>
    GarnetClient Create(GarnetOptions options);
}
