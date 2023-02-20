using System.Collections.Generic;

namespace OrchardCoreContrib.ContentLocalization.Transliteration;

public class DefaultTransliterateRuleProvider : ITransliterateRuleProvider
{
    private static readonly Dictionary<TransliterateScript, TransliterationRuleDelegate> _rules;

    static DefaultTransliterateRuleProvider()
    {
        _rules = new Dictionary<TransliterateScript, TransliterationRuleDelegate>();

        AddRule(TransliterateScript.Arabic, () => new Dictionary<string, string>
        {
            { "ا", "a" }, { "ب", "b" }, { "ت", "t" }, { "ث", "th" }, { "ج", "j" }, { "ح", "h" }, { "خ", "kh" }, { "د", "d" }, { "ذ", "dh" }, { "ر", "r" },
            { "ز", "z" }, { "س", "s" }, { "ش", "sh" }, { "ص", "s" }, { "ض", "d" }, { "ط", "t" }, { "ظ", "z" }, { "ع", "" }, { "غ", "gh" }, { "ف", "f" },
            { "ق", "q" }, { "ك", "k" }, { "ل", "l" }, { "م", "m" }, { "ن", "n" }, { "ه", "h" }, { "و", "w" },
            { "ي", "y" },
        });

        AddRule(TransliterateScript.Cyrillic, () => new Dictionary<string, string>
        {
            { "А", "A" }, { "Б", "B" }, { "В", "V" }, { "Г", "G" }, { "Д", "D" }, { "Е", "E" }, { "Ж", "Ž" }, { "З", "Z" }, { "И", "I" }, { "Й", "J" },
            { "К", "K" }, { "Л", "L" }, { "М", "M" }, { "Н", "N" }, { "О", "O" }, { "П", "P" }, { "Р", "R" }, { "С", "S" }, { "Т", "T" }, { "У", "U" },
            { "Ф", "F" }, { "Х", "H" }, { "Ц", "C" }, { "Ч", "C" }, { "Ш", "Š" }, { "Щ", "Š" }, { "Ь", "'" }, { "Э", "E" }, { "Ю", "U" }, { "Я", "A" },

            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" }, { "е", "e" }, { "ж", "ž" }, { "з", "z" }, { "и", "i" }, { "й", "j" },
            { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" },
            { "ф", "f" }, { "х", "h" }, { "ц", "c" }, { "ч", "č" }, { "ш", "" }, { "щ", "" }, { "ь", "'" }, { "э", "e" }, { "ю", "u" }, { "я", "a" }
        });
    }

    public bool TryGetRule(TransliterateScript script, out TransliterationRuleDelegate rule)
    {
        if (_rules.TryGetValue(script, out rule))
        {
            return true;
        }

        return false;
    }

    private static void AddRule(TransliterateScript script, TransliterationRuleDelegate rule)
    {
        _rules.Add(script, rule);
    }
}
