using System;
using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public static class DiacriticsLookupExtensions
    {
        public static IDictionary<char, string> Get(this IDiacriticsLookup diacriticsLookup, string culture)
        {
            if (diacriticsLookup is null)
            {
                throw new ArgumentNullException(nameof(diacriticsLookup));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"'{nameof(culture)}' cannot be null or empty.", nameof(culture));
            }

            var result = new Dictionary<char, string>();

            if (diacriticsLookup.Contains(culture))
            {
                result = (Dictionary<char, string>)diacriticsLookup[culture].Mapping;
            }

            return result;
        }
    }
}
