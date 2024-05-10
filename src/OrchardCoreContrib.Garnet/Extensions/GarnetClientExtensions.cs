using OrchardCoreContrib.Infrastructure;

namespace Garnet.client;

/// <summary>
/// Provides an extension methods for <see cref="GarnetClient"/>.
/// </summary>
public static class GarnetClientExtensions
{
    /// <summary>
    /// Gets the values of the specified set key.
    /// </summary>
    /// <param name="client">The <see cref="GarnetClient"/>.</param>
    /// <param name="key">The set key.</param>
    public static async Task<string[]> SetGetAsync(this GarnetClient client, string key)
    {
        Guard.ArgumentNotNull(client, nameof(client));
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));

        return await client.ExecuteForStringArrayResultAsync("SMEMBERS", [key]);
    }

    /// <summary>
    /// Adds a value to the specified set key.
    /// </summary>
    /// <param name="client">The <see cref="GarnetClient"/>.</param>
    /// <param name="key">The set key.</param>
    /// <param name="value">The value to be added.</param>
    /// <returns></returns>
    public static async Task SetSetAsync(this GarnetClient client, string key, string value)
    {
        Guard.ArgumentNotNull(client, nameof(client));
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));
        Guard.ArgumentNotNullOrEmpty(value, nameof(value));

        await client.ExecuteForStringResultAsync("SADD", [key, value]);
    }
}
