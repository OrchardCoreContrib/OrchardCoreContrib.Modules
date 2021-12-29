using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("ar");

        public IDictionary<char, char> Mapping => new Dictionary<char, char>
        {
            { 'آ', 'ا' },
            { 'أ', 'ا' },
            { 'ؤ', 'و' },
            { 'إ', 'ا' },
            { 'ئ', 'ي' },
            { 'ى', 'ي' },
            { 'ٱ', 'ا' }
        };
    }
}
