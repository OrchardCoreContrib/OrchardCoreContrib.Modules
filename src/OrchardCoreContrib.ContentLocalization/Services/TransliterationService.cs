using OrchardCoreContrib.ContentLocalization.Transliteration;
using System;

namespace OrchardCoreContrib.ContentLocalization.Services;

public class TransliterationService : ITransliterationService
{
    private readonly ITransliterateRuleProvider _transliterateRuleProvider;

    public TransliterationService(ITransliterateRuleProvider transliterateRuleProvider)
    {
        _transliterateRuleProvider = transliterateRuleProvider;
    }

    public string Transliterate(TransliterateScript script, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
        }

        if (_transliterateRuleProvider.TryGetRule(script, out var rule))
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
