using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class ArabicWawAccentMapper : AccentMapperBase
    {
        public ArabicWawAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('ؤ', "و"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("ar");
    }
}
