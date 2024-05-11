using Garnet.client;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Garnet;

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

    /// <summary>
    /// Gets the values of the specified list key.
    /// </summary>
    /// <param name="client">The <see cref="GarnetClient"/>.</param>
    /// <param name="key">The list key.</param>
    /// <param name="start">The offset start.</param>
    /// <param name="stop">The offset stop.</param>
    /// <returns></returns>
    public static async Task<string[]> ListRangeAsync(this GarnetClient client, string key, int start, int stop)
    {
        Guard.ArgumentNotNull(client, nameof(client));

        return await client.ExecuteForStringArrayResultAsync("LRANGE", [key, start.ToString(), stop.ToString()]);
    }

    /// <summary>
    /// Pushes a value to the tail of the list.
    /// </summary>
    /// <param name="client">The <see cref="GarnetClient"/>.</param>
    /// <param name="key">The list key.</param>
    /// <param name="value">The value to be added.</param>
    /// <returns></returns>
    public static async Task ListRightPushAsync(this GarnetClient client, string key, string value)
    {
        Guard.ArgumentNotNull(client, nameof(client));
        Guard.ArgumentNotNullOrEmpty(key, nameof(key));
        Guard.ArgumentNotNullOrEmpty(value, nameof(value));

        await client.ExecuteForStringResultAsync("RPUSH", [key, value]);
    }
}
