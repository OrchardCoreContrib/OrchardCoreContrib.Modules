namespace OrchardCoreContrib.ContentLocalization.Transliteration;

public interface ITransliterateRuleProvider
{
    bool TryGetRule(TransliterateScript script, out TransliterationRuleDelegate rule);
}
