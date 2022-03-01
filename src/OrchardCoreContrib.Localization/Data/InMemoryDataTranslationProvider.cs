using OrchardCore.Localization;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Provides a translations from memory.
    /// </summary>
    /// <remarks>This is for testing purpose.</remarks>
    public class InMemoryDataTranslationProvider : IDataTranslationProvider
    {
        private readonly static PluralizationRuleDelegate NoPluralRule = n => 0;

        private readonly IList<CultureDictionary> _dictionaries = new List<CultureDictionary>();

        /// <summary>
        /// Adds a set of translsations.
        /// </summary>
        /// <param name="dictionary">The <see cref="CultureDictionary"/> that contains the translations.</param>
        public void AddDictionary(CultureDictionary dictionary)
        {
            _dictionaries.Add(dictionary);
        }

        /// <summary>
        /// Adds a set of translsations.
        /// </summary>
        /// <param name="cultureName">The culture to accociate the records with.</param>
        /// <param name="records">A set of translations.</param>
        public void AddDictionary(string cultureName, IEnumerable<CultureDictionaryRecord> records)
        {
            var dictionary = new CultureDictionary(cultureName, NoPluralRule);
            dictionary.MergeTranslations(records);

            AddDictionary(dictionary);
        }

        /// <inheritdoc/>
        public void LoadTranslations(string cultureName, CultureDictionary dictionary)
        {
            var loadedDictionary = _dictionaries.SingleOrDefault(d => d.CultureName == cultureName);

            if (loadedDictionary != null)
            {
                dictionary.MergeTranslations(loadedDictionary);
            }
        }
    }
}
