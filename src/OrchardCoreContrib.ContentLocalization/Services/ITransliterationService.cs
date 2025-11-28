using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace OrchardCoreContrib.ContentLocalization.Services;

/// <summary>
/// Defines a service for converting text to a specified script using transliteration rules.
/// </summary>
public interface ITransliterationService
{
    /// <summary>
    /// Converts the specified text to the target script using transliteration rules.
    /// </summary>
    /// <param name="script">The script to which the text will be transliterated. Specifies the target writing system for the conversion.</param>
    /// <param name="text">The text to be transliterated. Cannot be null.</param>
    string Transliterate(TransliterateScript script, string text);
}
