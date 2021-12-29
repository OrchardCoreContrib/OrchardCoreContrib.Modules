using System.Globalization;
using System.Text;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public class DiacriticsRemover : IDiacriticsRemover
    {
        private readonly IDiacriticsLookup _diacriticsLookup;

        public DiacriticsRemover(IDiacriticsLookup diacriticsLookup)
        {
            _diacriticsLookup = diacriticsLookup;
        }

        public string Remove(string source)
        {
            var normalizedText = source.Normalize(NormalizationForm.FormKD);
            var result = new StringBuilder();

            for (var i = 0; i < normalizedText.Length; i++)
            {
                var currentChar = normalizedText[i];
                if (CharUnicodeInfo.GetUnicodeCategory(currentChar) == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                var currentCultureName = CultureInfo.CurrentUICulture.Name;
                if (_diacriticsLookup.Contains(currentCultureName))
                {
                    var mappedChars = _diacriticsLookup[currentCultureName].Mapping[currentChar];

                    if (mappedChars != null)
                    {
                        result.Append(mappedChars);
                        continue;
                    }
                }

                result.Append(currentChar);
            }

            return result.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
