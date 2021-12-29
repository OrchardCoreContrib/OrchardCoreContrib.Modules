using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicYaAccentMapper : AccentMapperBase
    {
        public ArabicYaAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('ئ', "ي"));
            Mapping.Add(new AccentDictionaryRecord('ى', "ي"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("ar");
    }
}
