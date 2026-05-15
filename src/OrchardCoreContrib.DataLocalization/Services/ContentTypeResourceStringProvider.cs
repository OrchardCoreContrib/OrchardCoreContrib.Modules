using OrchardCore.ContentTypes.Services;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization.Services;

/// <summary>
/// Represents a resource string provider for content types.
/// </summary>
/// <remarks>
/// Creates a instance of <see cref="ContentTypeResourceStringProvider"/>.
/// </remarks>
/// <param name="contentDefinitionService">The <see cref="IContentDefinitionService"/>.</param>
public class ContentTypeResourceStringProvider(IContentDefinitionService contentDefinitionService) : IDataResourceStringProvider
{
    internal static readonly string Context = "ContentType";

    /// <inheritdoc/>
    public async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
    {
        var contentTypes = await contentDefinitionService.GetTypesAsync();

        return contentTypes.Select(t => new CultureDictionaryRecordKey
        {
            MessageId = t.DisplayName,
            Context = Context
        });
    }
}
