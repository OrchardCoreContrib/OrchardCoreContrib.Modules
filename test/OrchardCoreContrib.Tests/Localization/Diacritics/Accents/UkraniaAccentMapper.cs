using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class UkraniaAccentMapper : IAccentMapper
    {
        public CultureInfo Culture => CultureInfo.GetCultureInfo("uk");

        public IDictionary<char, string> Mapping => new Dictionary<char, string>
            {
                { 'Ї',"І" },
                { 'Й',"И" },
                { 'й',"и" },
                { 'ї',"і" }
            };
    }
}
