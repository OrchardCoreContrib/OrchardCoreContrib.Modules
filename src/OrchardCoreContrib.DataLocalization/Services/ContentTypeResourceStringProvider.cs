using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization.Services;

/// <summary>
/// Represents a resource string provider for content types.
/// </summary>
/// <remarks>
/// Creates a instance of <see cref="ContentTypeResourceStringProvider"/>.
/// </remarks>
/// <param name="contentDefinitionManager">The <see cref="IContentDefinitionManager"/>.</param>
public class ContentTypeResourceStringProvider(IContentDefinitionManager contentDefinitionManager) : IDataResourceStringProvider
{
    internal static readonly string Context = "ContentType";

    /// <inheritdoc/>
    public async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
    {
        var contentTypes = await contentDefinitionManager.ListTypeDefinitionsAsync();

        return contentTypes.Select(t => new CultureDictionaryRecordKey
        {
            MessageId = t.DisplayName,
            Context = Context
        });
    }
}
