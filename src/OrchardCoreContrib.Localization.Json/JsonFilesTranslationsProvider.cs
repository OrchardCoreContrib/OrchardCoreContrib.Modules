using Microsoft.Extensions.FileProviders;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Localization.Json
{
    /// <summary>
    /// Represents a provider that provides a translations for .json files.
    /// </summary>
    public class JsonFilesTranslationsProvider : ITranslationProvider
    {
        private readonly ILocalizationFileLocationProvider _localizationFilesLocationProvider;
        private readonly JsonReader _jsonReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFilesTranslationsProvider"/> class.
        /// </summary>
        /// <param name="localizationFileLocationProvider">The <see cref="ILocalizationFileLocationProvider"/>.</param>
        public JsonFilesTranslationsProvider(ILocalizationFileLocationProvider localizationFileLocationProvider)
        {
            _localizationFilesLocationProvider = localizationFileLocationProvider;
            _jsonReader = new JsonReader();
        }

        /// <inheritdocs />
        public void LoadTranslations(string cultureName, CultureDictionary dictionary)
        {
            Guard.ArgumentNotNullOrEmpty(cultureName, nameof(cultureName));
            Guard.ArgumentNotNull(dictionary, nameof(dictionary));
            
            foreach (var fileInfo in _localizationFilesLocationProvider.GetLocations(cultureName))
            {
                LoadFileToDictionaryAsync(fileInfo, dictionary).GetAwaiter().GetResult();
            }
        }

        private async Task LoadFileToDictionaryAsync(IFileInfo fileInfo, CultureDictionary dictionary)
        {
            if (fileInfo.Exists && !fileInfo.IsDirectory)
            {
                using (var stream = fileInfo.CreateReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var cultureRecords = await _jsonReader.ParseAsync(stream);
                        dictionary.MergeTranslations(cultureRecords);
                    }
                }
            }
        }
    }
}
