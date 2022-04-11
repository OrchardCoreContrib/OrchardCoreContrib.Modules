using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentTypes.Services;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings()
            => _contentDefinitionService.GetTypes()
                .SelectMany(t => t.TypeDefinition.Parts
                    .SelectMany(p => p.PartDefinition.Fields
                        .Select(f => new { ContentType = t.Name, ContentField = f.GetSettings<ContentPartFieldSettings>().DisplayName })))
                .Select(t => new CultureDictionaryRecordKey(t.ContentField, $"{t.ContentType}-{Context}"));
    }
}
