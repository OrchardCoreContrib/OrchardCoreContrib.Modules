using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Localization.Data;

/// <summary>
/// Provides an extension methods for <see cref="IDataResourceStringProvider"/>.
/// </summary>
public static class DataResourceStringProviderExtensions
{
    /// <summary>
    /// Gets the resource strings.
    /// </summary>
    /// <param name="resourceStringProvider">The <see cref="IDataResourceStringProvider"/>.</param>
    /// <param name="context">The resource context.</param>
    [Obsolete("This method is obsolete. Use GetAllResourceStringsAsync() instead.")]
    public static IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings(this IDataResourceStringProvider resourceStringProvider, string context)
        => GetAllResourceStringsAsync(resourceStringProvider, context).GetAwaiter().GetResult();

    /// <summary>
    /// Gets the resource strings.
    /// </summary>
    /// <param name="resourceStringProvider">The <see cref="IDataResourceStringProvider"/>.</param>
    /// <param name="context">The resource context.</param>
    public static async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync(this IDataResourceStringProvider resourceStringProvider, string context)
    {
        Guard.ArgumentNotNull(resourceStringProvider, nameof(resourceStringProvider));
        Guard.ArgumentNotNullOrEmpty(context, nameof(context));

        var resourceStrings = await resourceStringProvider.GetAllResourceStringsAsync();

        return resourceStrings.Where(s => s.GetContext().Equals(context, StringComparison.OrdinalIgnoreCase));
    }
}
