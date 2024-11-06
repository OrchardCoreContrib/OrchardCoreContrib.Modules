using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides a transliteration service extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class TransliterationServiceCollectionExtensions
{
    /// <summary>
    /// Adds the transliteration services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    public static IServiceCollection AddTransliteration(this IServiceCollection services)
    {
        services.AddSingleton<ITransliterateRuleProvider, DefaultTransliterateRuleProvider>();

        return services;
    }
}
