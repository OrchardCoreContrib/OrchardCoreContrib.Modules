using System;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public static class DiacriticsLookupExtensions
    {
        public static AccentDictionary Get(this IDiacriticsLookup diacriticsLookup, string culture)
        {
            if (diacriticsLookup is null)
            {
                throw new ArgumentNullException(nameof(diacriticsLookup));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"'{nameof(culture)}' cannot be null or empty.", nameof(culture));
            }

            var result = new AccentDictionary(culture);

            if (diacriticsLookup.Contains(culture))
            {
                result = diacriticsLookup[culture].Mapping;
            }

            return result;
        }
    }
}
