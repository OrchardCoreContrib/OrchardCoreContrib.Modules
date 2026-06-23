using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization.Services;

/// <summary>
/// Represents a resource string provider for content fields.
/// </summary>
/// <remarks>
/// Creates a instance of <see cref="ContentFieldResourceStringProvider"/>.
/// </remarks>
public class ContentFieldResourceStringProvider(
    IContentDefinitionManager contentDefinitionManager) : IDataResourceStringProvider
{
    internal static readonly string Context = "ContentField";

    /// <inheritdoc/>
    public async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
    {
        var cultureDictionary = new List<CultureDictionaryRecordKey>();

        var typeDefinitions = await contentDefinitionManager.ListTypeDefinitionsAsync();

        foreach (var typeDefinition in typeDefinitions)
        {
            var fields = await GetFieldNamesAsync(typeDefinition.Name);

            cultureDictionary.AddRange(fields.Select(field => new CultureDictionaryRecordKey
            {
                MessageId = field,
                Context = $"{typeDefinition.Name}-{Context}"
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
