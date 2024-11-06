namespace OrchardCoreContrib.ContentLocalization.Transliteration;

/// <summary>
/// Represents a contract for providing transliteration rules.
/// </summary>
public interface ITransliterateRuleProvider
{
    /// <summary>
    /// Try to get the transliteration rule for the specified <see cref="TransliterateScript"/>.
    /// </summary>
    /// <param name="script">The <see cref="TransliterateScript"/>.</param>
    /// <param name="rule">The</param>
    bool TryGetRule(TransliterateScript script, out TransliterationRuleDelegate rule);
}
