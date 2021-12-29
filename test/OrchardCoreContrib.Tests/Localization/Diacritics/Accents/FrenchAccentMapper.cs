using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Accents.Tests
{
    public class FrenchAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("ar");

        public IDictionary<char, string> Mapping => new Dictionary<char, string>
        {
            { 'æ', "ae" },
            { 'œ', "oe" }
        };
    }
}
