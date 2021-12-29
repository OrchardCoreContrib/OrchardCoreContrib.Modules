using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicAlifAccentMapper : AccentMapperBase
    {
        public ArabicAlifAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('آ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('أ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('إ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('ٱ', "ا"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("ar");
    }
}
