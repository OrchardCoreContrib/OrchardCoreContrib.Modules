using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace Microsoft.Extensions.DependencyInjection;

public static class TransliterationServiceCollectionExtensions
{
    public static IServiceCollection AddTransliteration(this IServiceCollection services)
    {
        services.AddSingleton<ITransliterateRuleProvider, DefaultTransliterateRuleProvider>();

        return services;
    }
}
