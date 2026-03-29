using OrchardCoreContrib.DataLocalization.Models;
using OrchardCore.Documents;

namespace OrchardCoreContrib.DataLocalization.Services;

/// <summary>
/// Manages  
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="TranslationsManager"/>.
/// </remarks>
/// <param name="documentManager"></param>
public class TranslationsManager(IDocumentManager<TranslationsDocument> documentManager)
{
    /// <summary>
    /// Loads the translations document from the store for updating and that should not be cached.
    /// </summary>
    public Task<TranslationsDocument> LoadTranslationsDocumentAsync() => documentManager.GetOrCreateMutableAsync();

    /// <summary>
    /// Gets the translations document from the cache for sharing and that should not be updated.
    /// </summary>
    public Task<TranslationsDocument> GetTranslationsDocumentAsync() => documentManager.GetOrCreateImmutableAsync();

    /// <summary>
    /// Updates the translations for a given culture.
    /// </summary>
    /// <param name="culture">The culture for the updated transaltions.</param>
    /// <param name="translations">The translations to be updated.</param>
    public async Task UpdateTranslationAsync(string culture, IEnumerable<Translation> translations)
    {
        var document = await LoadTranslationsDocumentAsync();
        
        document.Translations[culture] = translations;

        await documentManager.UpdateAsync(document);
    }
}
