namespace OrchardCoreContrib.Localization.Diacritics
{
    public interface IDiacriticsLookup
    {
        IAccentMapper this[string culture] { get; }

        public int Count { get; }

        bool Contains(string culture);
    }
}
