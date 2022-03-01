using Microsoft.Extensions.Caching.Memory;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a resource manager that provides convenient access to culture-specific resources from the data store.
    /// </summary>
    public class DataResourceManager
    {
        private const string CacheKeyPrefix = "CultureDictionary-";

        private static readonly PluralizationRuleDelegate DefaultPluralRule = n => (n != 1 ? 1 : 0);

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
        public string GetString(string name) => GetString(name, null);

        /// <summary>
        /// Gets the resource value from a given name and culture.
        /// </summary>
        /// <param name="name">The resource name to be retrieved.</param>
        /// <param name="culture">The culture that has been used to retrieve the resource.</param>
        public string GetString(string name, CultureInfo culture)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (culture == null)
            {
                culture = CultureInfo.CurrentUICulture;
            }

            string value = null;
            var dictionary = GetCultureDictionary(culture);

            if (dictionary != null)
            {
                var key = new CultureDictionaryRecordKey(name);

                value = dictionary[key];
            }

            return value;
        }

        /// <summary>
        /// Gets the localization resources from underlying store with a given culture.
        /// </summary>
        /// <param name="culture">The culture that has been used to retrieve the resources.</param>
        public virtual IDictionary<CultureDictionaryRecordKey, string[]> GetResources(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var dictionary = GetCultureDictionary(culture);

            return dictionary.Translations;
        }

        private CultureDictionary GetCultureDictionary(CultureInfo culture)
        {
            var cachedDictionary = _cache.GetOrCreate(CacheKeyPrefix + culture.Name, k => new Lazy<CultureDictionary>(() =>
            {
                var dictionary = new CultureDictionary(culture.Name, DefaultPluralRule);

                _translationProvider.LoadTranslations(culture.Name, dictionary);

                return dictionary;
            }, LazyThreadSafetyMode.ExecutionAndPublication));

            return cachedDictionary.Value;
        }
    }
}
