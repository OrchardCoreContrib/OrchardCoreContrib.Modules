using Microsoft.Extensions.Caching.Memory;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Data;

/// <summary>
/// Represents a resource manager that provides convenient access to culture-specific resources from the data store.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="DataResourceManager"/>.
/// </remarks>
/// <param name="translationProvider">The <see cref="IDataTranslationProvider"/>.</param>
/// <param name="cache">The <see cref="IMemoryCache"/>.</param>
public class DataResourceManager(IDataTranslationProvider translationProvider, IMemoryCache cache)
{
    private const string CacheKeyPrefix = "OCC-CultureDictionary-";

    private static readonly PluralizationRuleDelegate NoPluralRule = n => 0;

    /// <summary>
    /// Gets the resource value from a given name.
    /// </summary>
    /// <param name="name">The resource name to be retrieved.</param>
    /// <param name="context">The resource context to be retrieved.</param>
    public string GetString(string name, string context) => GetString(name, context, null);

    /// <summary>
    /// Gets the resource value from a given name and culture.
    /// </summary>
    /// <param name="name">The resource name to be retrieved.</param>
    /// <param name="culture">The culture that has been used to retrieve the resource.</param>
    public string GetString(string name, string context, CultureInfo culture)
    {
        Guard.ArgumentNotNullOrEmpty(name, nameof(name));
        Guard.ArgumentNotNullOrEmpty(context, nameof(context));

        culture ??= CultureInfo.CurrentUICulture;

        string value = null;
        var dictionary = GetCultureDictionary(culture);


        if (dictionary != null)
        {
            var key = CultureDictionaryRecord.GetKey(name, context);

            value = dictionary[key];
        }

        return value;
    }

    /// <summary>
    /// Gets the localization resources from underlying store with a given culture.
    /// </summary>
    /// <param name="culture">The culture that has been used to retrieve the resources.</param>
    /// <param name="tryParents">Whether to use resource fallback if the resources can't be found.</param>
    public IDictionary<string, string> GetResources(CultureInfo culture, bool tryParents)
    {
        Guard.ArgumentNotNull(culture, nameof(culture));

        var currentCulture = culture;

        var resources = GetCultureDictionary(culture).Translations
            .ToDictionary(t => t.Key.ToString(), t => t.Value[0]);

        if (tryParents)
        {
            do
            {
                currentCulture = currentCulture.Parent;

                var currentResources = GetCultureDictionary(currentCulture).Translations
                    .ToDictionary(t => t.Key.ToString(), t => t.Value[0]);

                foreach (var translation in currentResources)
                {
                    if (!resources.Any(r => r.Key == translation.Key))
                    {
                        resources.Add(translation.Key, translation.Value);
                    }
                }
            } while (currentCulture != CultureInfo.InvariantCulture);
        }

        return resources;
    }

    private CultureDictionary GetCultureDictionary(CultureInfo culture)
    {
        var cachedDictionary = cache.GetOrCreate(CacheKeyPrefix + culture.Name, k => new Lazy<CultureDictionary>(() =>
        {
            var dictionary = new CultureDictionary(culture.Name, NoPluralRule);

            translationProvider.LoadTranslations(culture.Name, dictionary);

            return dictionary;
        }, LazyThreadSafetyMode.ExecutionAndPublication));

        return cachedDictionary.Value;
    }
}
