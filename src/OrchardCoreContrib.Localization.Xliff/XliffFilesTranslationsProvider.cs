using Microsoft.Extensions.FileProviders;
using OrchardCore.Localization;
using System.IO;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents a provider that provides a translations for .XLIFF files.
/// </summary>
public class XliffFilesTranslationsProvider : ITranslationProvider
{
    private readonly ILocalizationFileLocationProvider _localizationFilesLocationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="XliffFilesTranslationsProvider"/> class.
    /// </summary>
    /// <param name="localizationFileLocationProvider">The <see cref="ILocalizationFileLocationProvider"/>.</param>
    public XliffFilesTranslationsProvider(ILocalizationFileLocationProvider localizationFileLocationProvider)
    {
        _localizationFilesLocationProvider = localizationFileLocationProvider;
    }

    /// <inheritdocs />
    public void LoadTranslations(string cultureName, CultureDictionary dictionary)
    {
        foreach (var fileInfo in _localizationFilesLocationProvider.GetLocations(cultureName))
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
