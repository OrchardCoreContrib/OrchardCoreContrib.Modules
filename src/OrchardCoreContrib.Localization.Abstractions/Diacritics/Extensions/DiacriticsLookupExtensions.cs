using OrchardCoreContrib.Infrastructure;
using System;

namespace OrchardCoreContrib.Localization.Diacritics
{
    /// <summary>
    /// Represents an extension methods for <see cref="IDiacriticsLookup"/>.
    /// </summary>
    public static class DiacriticsLookupExtensions
    {
        /// <summary>
        /// Gets a list of accents associated with a given culture.
        /// </summary>
        /// <param name="diacriticsLookup">The <see cref="IDiacriticsLookup"/>.</param>
        /// <param name="culture">The culture to get its diacritics.</param>
        /// <returns>An accent dictionary for a given culture.</returns>
        public static AccentDictionary Get(this IDiacriticsLookup diacriticsLookup, string culture)
        {
            Guard.ArgumentNotNull(diacriticsLookup, nameof(diacriticsLookup));
            Guard.ArgumentNotNullOrEmpty(culture, nameof(culture));

            var result = new AccentDictionary(culture);

            if (diacriticsLookup.Contains(culture))
            {
                result = diacriticsLookup[culture].Mapping;
            }

            return result;
        }
    }
}
