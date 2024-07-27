using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using OrchardCoreContrib.ContentLocalization.Liquid;

namespace OrchardCoreContrib.ContentLocalization.Extensions;

public static class LiquidFilterServicesExtensions
{
    public static void AddLiquidFilters(this IServiceCollection services)
    {
        services.AddLiquidFilter<CyrToLatFilter>("cyr_to_lat");
        services.AddLiquidFilter<ArabToLatFilter>("arab_to_lat");
    }
}