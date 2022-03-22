using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Localization;
using OrchardCoreContrib.DataLocalization.Services;
using OrchardCoreContrib.Localization.Data;
using System.Linq;

namespace OrchardCoreContrib.DataLocalization
{
    /// <summary>
    /// Provides a translations from the underlying data store.
    /// </summary>
    public class DataTranslationProvider : IDataTranslationProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Creates a new instance of <see cref="DataTranslationProvider"/>.
        /// </summary>
        /// <param name="scopeFactory">The <see cref="IServiceScopeFactory"/>.</param>
        public DataTranslationProvider(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <inheritdoc/>
        public void LoadTranslations(string cultureName, CultureDictionary dictionary)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var translationsManager = scope.ServiceProvider.GetService<TranslationsManager>();

                var translationsDocument = translationsManager.GetTranslationsDocumentAsync().Result;

                if (translationsDocument.Translations.ContainsKey(cultureName))
                {
                    var records = translationsDocument.Translations[cultureName]
                        .Select(t => new CultureDictionaryRecord(t.Key, t.Context, new[] { t.Value }));

                    dictionary.MergeTranslations(records);
                }
            }
        }
    }
}
