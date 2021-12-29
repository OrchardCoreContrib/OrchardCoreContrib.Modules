using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class UkraniaAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("uk");

        public IDictionary<char, char> Mapping => new Dictionary<char, char>
            {
                { 'Ї','І' },
                { 'Й','И' },
                { 'й','и' },
                { 'ї','і' }
            };
    }
}
