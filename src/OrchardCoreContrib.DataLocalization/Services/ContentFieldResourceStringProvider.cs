using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentTypes.Services;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization.Services;

/// <summary>
/// Represents a resource string provider for content fields.
/// </summary>
/// <remarks>
/// Creates a instance of <see cref="ContentTypeResourceStringProvider"/>.
/// </remarks>
/// <param name="contentDefinitionService">The <see cref="IContentDefinitionService"/>.</param>
public class ContentFieldResourceStringProvider(
    IContentDefinitionService contentDefinitionService,
    IContentDefinitionManager contentDefinitionManager) : IDataResourceStringProvider
{
    internal static readonly string Context = "ContentField";

    /// <inheritdoc/>
    public async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
    {
        var cultureDictionary = new List<CultureDictionaryRecordKey>();

        var typeViewModels = await contentDefinitionService.GetTypesAsync();

        foreach (var typeViewModel in typeViewModels)
        {
            var fields = await GetFieldNamesAsync(typeViewModel.TypeDefinition.Name);

            cultureDictionary.AddRange(fields.Select(field => new CultureDictionaryRecordKey
            {
                MessageId = field,
                Context = $"{typeViewModel.Name}-{Context}"
            }));
        }

        return cultureDictionary;
    }

    private async Task<IEnumerable<string>> GetFieldNamesAsync(string contentType)
    {
        var typeDefinition = await contentDefinitionManager.GetTypeDefinitionAsync(contentType);
        
        if (typeDefinition is null)
        {
            return [];
        }

        return typeDefinition.Parts
            .SelectMany(p => p.PartDefinition.Fields)
            .Select(f => f.Name);
    }
}
