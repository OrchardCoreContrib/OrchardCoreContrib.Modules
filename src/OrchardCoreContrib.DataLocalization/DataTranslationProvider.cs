using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Localization;
using OrchardCoreContrib.DataLocalization.Services;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization;

/// <summary>
/// Provides a translations from the underlying data store.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="DataTranslationProvider"/>.
/// </remarks>
/// <param name="scopeFactory">The <see cref="IServiceScopeFactory"/>.</param>
public class DataTranslationProvider(IServiceScopeFactory scopeFactory) : IDataTranslationProvider
{
    /// <inheritdoc/>
    public void LoadTranslations(string cultureName, CultureDictionary dictionary)
    {
        using (var scope = scopeFactory.CreateScope())
        {
            var translationsManager = scope.ServiceProvider.GetService<TranslationsManager>();

            var translationsDocument = translationsManager.GetTranslationsDocumentAsync().Result;

            if (translationsDocument.Translations.TryGetValue(cultureName, out IEnumerable<Models.Translation> value))
            {
                var records = value.Select(t => new CultureDictionaryRecord(t.Key, t.Context, [t.Value]));

                dictionary.MergeTranslations(records);
            }
        }
    }
}
