using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCoreContrib.ContentLocalization.Liquid;
using OrchardCoreContrib.ContentLocalization.Services;

namespace OrchardCoreContrib.ContentLocalization;

[Feature("OrchardCoreContrib.ContentLocalization.Transliteration")]
[RequireFeatures("OrchardCoreContrib.Liquid")]
public class TransliterationStartup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransliteration();

        services.AddScoped<ITransliterationService, TransliterationService>();

        services.AddLiquidFilter<CyrToLatFilter>("cyr_to_lat");
        services.AddLiquidFilter<ArabToLatFilter>("arab_to_lat");
    }
}
