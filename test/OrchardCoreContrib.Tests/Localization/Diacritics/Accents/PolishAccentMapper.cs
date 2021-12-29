using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class PolishAccentMapper : AccentMapperBase
    {
        public PolishAccentMapper()
        {
            //Mapping.Add(new AccentDictionaryRecord('Ó', "O"));
            //Mapping.Add(new AccentDictionaryRecord('ó', "o"));
            //Mapping.Add(new AccentDictionaryRecord('Ą', "A"));
            //Mapping.Add(new AccentDictionaryRecord('ą', "a"));
            //Mapping.Add(new AccentDictionaryRecord('Ć', "C"));
            //Mapping.Add(new AccentDictionaryRecord('ć', "c"));
            //Mapping.Add(new AccentDictionaryRecord('Ę', "E"));
            //Mapping.Add(new AccentDictionaryRecord('ę', "e"));
            Mapping.Add(new AccentDictionaryRecord('Ł', "L"));
            Mapping.Add(new AccentDictionaryRecord('ł', "l"));
            //Mapping.Add(new AccentDictionaryRecord('Ń', "N"));
            //Mapping.Add(new AccentDictionaryRecord('ń', "n"));
            //Mapping.Add(new AccentDictionaryRecord('Ś', "S"));
            //Mapping.Add(new AccentDictionaryRecord('ś', "s"));
            //Mapping.Add(new AccentDictionaryRecord('Ź', "Z"));
            //Mapping.Add(new AccentDictionaryRecord('ź', "z"));
            //Mapping.Add(new AccentDictionaryRecord('Ż', "Z"));
            //Mapping.Add(new AccentDictionaryRecord('ż', "z"));
        }

        public override CultureInfo Culture => CultureInfo.GetCultureInfo("pl");
    }
}
