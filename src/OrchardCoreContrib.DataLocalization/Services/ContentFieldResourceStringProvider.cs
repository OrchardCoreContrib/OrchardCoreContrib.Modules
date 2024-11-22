using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentTypes.Services;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.DataLocalization.Services
{
    /// <summary>
    /// Represents a resource string provider for content fields.
    /// </summary>
    public class ContentFieldResourceStringProvider : IDataResourceStringProvider
    {
        internal static readonly string Context = "ContentField";

        private readonly IContentDefinitionService _contentDefinitionService;

        /// <summary>
        /// Creates a instance of <see cref="ContentTypeResourceStringProvider"/>.
        /// </summary>
        /// <param name="contentDefinitionService">The <see cref="IContentDefinitionService"/>.</param>
        public ContentFieldResourceStringProvider(IContentDefinitionService contentDefinitionService)
        {
            _contentDefinitionService = contentDefinitionService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
        {
            var contentTypes = await _contentDefinitionService.GetTypesAsync();
            
            return contentTypes
                .SelectMany(t => t.TypeDefinition.Parts.SelectMany(p => p.PartDefinition.Fields.Select(f => new { ContentType = t.Name, ContentField = f.GetSettings<ContentPartFieldSettings>().DisplayName })))
                .Select(t => new CultureDictionaryRecordKey
                {
                    MessageId = t.ContentField,
                    Context = $"{t.ContentType}-{Context}"
                });
        }
    }
}
