using OrchardCore.Localization;
using System.Linq;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Provides a translations from the underlying data store.
    /// </summary>
    public class DataTranslationProvider : IDataTranslationProvider
    {
        /// <inheritdoc/>
        public void LoadTranslations(string cultureName, CultureDictionary dictionary)
        {
            // TODO: Load the translations from the database
            var records = Enumerable.Empty<CultureDictionaryRecord>();
            
            dictionary.MergeTranslations(records);
        }
    }
}
