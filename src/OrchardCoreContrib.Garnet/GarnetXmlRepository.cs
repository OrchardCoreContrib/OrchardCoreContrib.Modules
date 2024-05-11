using Garnet.client;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Xml.Linq;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a repository to store data protection keys in Garnet.
/// </summary>
/// <param name="garnetClientFactory">The <see cref="GarnetClient"/> factory.</param>
/// <param name="key">The key.</param>
public class GarnetXmlRepository(Func<GarnetClient> garnetClientFactory, string key) : IXmlRepository
{
    /// <inheritdoc/>
    public IReadOnlyCollection<XElement> GetAllElements() => GetAllElementsCore().ToList().AsReadOnly();

    /// <inheritdoc/>
    public void StoreElement(XElement element, string friendlyName) => garnetClientFactory()
        .ListRightPushAsync(key, element.ToString(SaveOptions.DisableFormatting))
        .GetAwaiter()
        .GetResult();

    private IEnumerable<XElement> GetAllElementsCore()
    {
        var elements = garnetClientFactory()
            .ListRangeAsync(key, 0, -1)
            .GetAwaiter()
            .GetResult();

        foreach (var element in elements)
        {
            yield return XElement.Parse(element);
        }
    }
}
