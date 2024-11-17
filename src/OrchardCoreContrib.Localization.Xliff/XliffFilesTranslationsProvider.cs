using Microsoft.Extensions.FileProviders;
using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents a provider that provides a translations for .XLIFF files.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="XliffFilesTranslationsProvider"/> class.
/// </remarks>
/// <param name="localizationFileLocationProvider">The <see cref="ILocalizationFileLocationProvider"/>.</param>
public class XliffFilesTranslationsProvider(ILocalizationFileLocationProvider localizationFileLocationProvider) : ITranslationProvider
{
    /// <inheritdocs />
    public void LoadTranslations(string cultureName, CultureDictionary dictionary)
    {
        foreach (var fileInfo in localizationFileLocationProvider.GetLocations(cultureName))
        {
            LoadFileToDictionaryAsync(fileInfo, dictionary).GetAwaiter().GetResult();
        }
    }

    private static async Task LoadFileToDictionaryAsync(IFileInfo fileInfo, CultureDictionary dictionary)
    {
        if (fileInfo.Exists && !fileInfo.IsDirectory)
        {
            using var stream = fileInfo.CreateReadStream();
            using var reader = new StreamReader(stream);
            var cultureRecords = await XliffReader.ParseAsync(stream);

            dictionary.MergeTranslations(cultureRecords);
        }
    }
}
