using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicYaAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("ar");

        public IDictionary<char, char> Mapping => new Dictionary<char, char>
        {
            { 'ئ', 'ي' },
            { 'ى', 'ي' }
        };
    }
}
