using Microsoft.Extensions.Caching.Memory;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a resource manager that provides convenient access to culture-specific resources from the data store.
    /// </summary>
    public class DataResourceManager
    {
        private const string CacheKeyPrefix = "OCC-CultureDictionary-";

        private static readonly PluralizationRuleDelegate NoPluralRule = n => 0;

        private readonly IDataTranslationProvider _translationProvider;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of <see cref="DataResourceManager"/>.
        /// </summary>
        /// <param name="translationProvider">The <see cref="IDataTranslationProvider"/>.</param>
        /// <param name="cache">The <see cref="IMemoryCache"/>.</param>
        public DataResourceManager(IDataTranslationProvider translationProvider, IMemoryCache cache)
        {
            _translationProvider = translationProvider;
            _cache = cache;
        }

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

            if (culture == null)
            {
                culture = CultureInfo.CurrentUICulture;
            }

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
            var cachedDictionary = _cache.GetOrCreate(CacheKeyPrefix + culture.Name, k => new Lazy<CultureDictionary>(() =>
            {
                var dictionary = new CultureDictionary(culture.Name, NoPluralRule);

                _translationProvider.LoadTranslations(culture.Name, dictionary);

                return dictionary;
            }, LazyThreadSafetyMode.ExecutionAndPublication));

            return cachedDictionary.Value;
        }
    }
}
