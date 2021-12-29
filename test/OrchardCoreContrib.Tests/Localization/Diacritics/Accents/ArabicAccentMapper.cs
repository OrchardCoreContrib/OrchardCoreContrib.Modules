using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("ar");

        public IDictionary<char, string> Mapping => new Dictionary<char, string>
        {
            { 'آ', "ا" },
            { 'أ', "ا" },
            { 'ؤ', "و" },
            { 'إ', "ا" },
            { 'ئ', "ي" },
            { 'ى', "ي" },
            { 'ٱ', "ا" }
        };
    }
}
