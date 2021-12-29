using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public interface IAccentMapper
    {
        CultureInfo Culture { get; }

        AccentDictionary Mapping { get; }
    }
}
