using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public abstract class AccentMapperBase : IAccentMapper
    {
        protected AccentMapperBase()
        {
            Mapping = new AccentDictionary(Culture.Name);
        }

        public abstract CultureInfo Culture { get; }

        public AccentDictionary Mapping { get; }
    }
}
