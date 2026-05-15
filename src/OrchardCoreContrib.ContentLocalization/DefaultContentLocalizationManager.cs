using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentLocalization.Handlers;
using OrchardCore.ContentLocalization.Records;
using OrchardCore.ContentManagement;
using OrchardCore.Localization;
using YesSql;

namespace OrchardCoreContrib.ContentLocalization;

public class DefaultContentLocalizationManager(
    IContentManager contentManager,
    YesSql.ISession session,
    IHttpContextAccessor httpContentAccessor,
    ILocalizationService localizationService,
    ILogger<DefaultContentLocalizationManager> logger, IEnumerable<IContentLocalizationHandler> handlers, OrchardCore.Entities.IIdGenerator iidGenerator)
        : OrchardCore.ContentLocalization.DefaultContentLocalizationManager(
            contentManager, session, httpContentAccessor, localizationService, logger, handlers, iidGenerator),
    IContentLocalizationManager
{
    public async Task<IEnumerable<string>> GetSetsAsync()
    {
        var indexValues = await session
            .QueryIndex<LocalizedContentItemIndex>(i => (i.Published || i.Latest))
            .ListAsync();

        return indexValues
            .GroupBy(s => s.LocalizationSet)
            .Select(s => s.Key);
    }
}
