using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace OrchardCoreContrib.ContentLocalization.Services;

public interface ITransliterationService
{
    string Transliterate(TransliterateScript script, string text);
}
