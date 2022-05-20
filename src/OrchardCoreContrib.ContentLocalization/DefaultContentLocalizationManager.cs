using Microsoft.Extensions.Logging;
using OrchardCore.ContentLocalization.Handlers;
using OrchardCore.ContentLocalization.Records;
using OrchardCore.ContentManagement;
using OrchardCore.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace OrchardCoreContrib.ContentLocalization
{
    public class DefaultContentLocalizationManager
        : OrchardCore.ContentLocalization.DefaultContentLocalizationManager,
        IContentLocalizationManager
    {
        private readonly ISession _session;

        public DefaultContentLocalizationManager(IContentManager contentManager, ISession session, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContentAccessor, ILocalizationService localizationService, ILogger<OrchardCore.ContentLocalization.DefaultContentLocalizationManager> logger, IEnumerable<IContentLocalizationHandler> handlers, OrchardCore.Entities.IIdGenerator iidGenerator) : base(contentManager, session, httpContentAccessor, localizationService, logger, handlers, iidGenerator)
        {
            _session = session;
        }

        public async Task<IEnumerable<string>> GetSetsAsync()
        {
            var indexValues = await _session
                .QueryIndex<LocalizedContentItemIndex>(i => (i.Published || i.Latest))
                .ListAsync();

            return indexValues
                .GroupBy(s => s.LocalizationSet)
                .Select(s => s.Key);
        }
    }
}
