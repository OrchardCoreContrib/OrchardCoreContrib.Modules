using OrchardCoreContrib.ContentLocalization.Transliteration;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.ContentLocalization.Services;

/// <summary>
/// Provides transliteration services for converting text between different scripts using configurable transliteration
/// rules.
/// </summary>
/// <param name="transliterateRuleProvider">The provider that supplies transliteration rules for supported scripts. Cannot be null.</param>
public class TransliterationService(ITransliterateRuleProvider transliterateRuleProvider) : ITransliterationService
{
    /// <inheritdoc/>
    public string Transliterate(TransliterateScript script, string text)
    {
        Guard.ArgumentNotNullOrEmpty(text, nameof(text));

        if (transliterateRuleProvider.TryGetRule(script, out var rule))
        {
            var mappings = rule.Invoke();

            var result = String.Empty;
            foreach (var c in text)
            {
                var key = c.ToString();
                if (!mappings.TryGetValue(key, out var value))
                {
                    value = key;
                }
                
                result += value;
            }

            return result;
        }

        return text;
    }
}
