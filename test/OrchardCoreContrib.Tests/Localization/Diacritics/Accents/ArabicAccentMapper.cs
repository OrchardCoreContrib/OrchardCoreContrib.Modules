using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicAccentMapper : AccentMapperBase
    {
        public ArabicAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('آ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('أ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('ؤ', "و"));
            Mapping.Add(new AccentDictionaryRecord('إ', "ا"));
            Mapping.Add(new AccentDictionaryRecord('ئ', "ي"));
            Mapping.Add(new AccentDictionaryRecord('ى', "ي"));
            Mapping.Add(new AccentDictionaryRecord('ٱ', "ا"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("ar");
    }
}
