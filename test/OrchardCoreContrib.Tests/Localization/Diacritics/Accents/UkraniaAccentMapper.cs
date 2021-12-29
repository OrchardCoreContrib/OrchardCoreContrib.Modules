using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class UkraniaAccentMapper : AccentMapperBase
    {
        public UkraniaAccentMapper()
        {
            Mapping.Add(new AccentDictionaryRecord('Ї', "І"));
            Mapping.Add(new AccentDictionaryRecord('Й', "И"));
            Mapping.Add(new AccentDictionaryRecord('й', "и"));
            Mapping.Add(new AccentDictionaryRecord('ї', "і"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("uk");
    }
}
