using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class FrenchAccentMapper : AccentMapperBase
    {
        public FrenchAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('æ', "ae"));
            Mapping.Add(new AccentDictionaryRecord('œ', "oe"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("fr");
    }
}
