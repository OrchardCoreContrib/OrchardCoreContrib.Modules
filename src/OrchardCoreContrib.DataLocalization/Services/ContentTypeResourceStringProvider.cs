using OrchardCore.ContentTypes.Services;
using OrchardCore.Localization;
using OrchardCoreContrib.Localization.Data;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.DataLocalization.Services
{
    /// <summary>
    /// Represents a resource string provider for content types.
    /// </summary>
    public class ContentTypeResourceStringProvider : IDataResourceStringProvider
    {
        internal static readonly string Context = "ContentType";

        private readonly IContentDefinitionService _contentDefinitionService;

        /// <summary>
        /// Creates a instance of <see cref="ContentTypeResourceStringProvider"/>.
        /// </summary>
        /// <param name="contentDefinitionService">The <see cref="IContentDefinitionService"/>.</param>
        public ContentTypeResourceStringProvider(IContentDefinitionService contentDefinitionService)
        {
            _contentDefinitionService = contentDefinitionService;
        }

        /// <inheritdoc/>
        public IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings()
        {
            // Change once https://github.com/OrchardCoreContrib/OrchardCoreContrib/issues/12 fixed
            var contentTypes = _contentDefinitionService.GetTypesAsync().GetAwaiter().GetResult();

            return contentTypes.Select(t => new CultureDictionaryRecordKey(t.DisplayName, Context));
        }
    }
}
