namespace OrchardCoreContrib.Localization.Diacritics
{
    /// <summary>
    /// Represents a contract for diacritics lookup.
    /// </summary>
    public interface IDiacriticsLookup
    {
        /// <summary>
        /// Gets the mapped accent for a given culture.
        /// </summary>
        /// <param name="culture">The culture to retrieve its accent mapper.</param>
        IAccentMapper this[string culture] { get; }

        /// <summary>
        /// Gets the number of the accents mapper in the lookup table.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets whether a given culture is exists in the lookup table or not.
        /// </summary>
        /// <param name="culture">The culture to be checked.</param>
        bool Contains(string culture);
    }
}
