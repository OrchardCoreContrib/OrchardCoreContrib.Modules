using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public interface IAccentMapper
    {
        CultureInfo Culture { get; }

        IDictionary<char, string> Mapping { get; }
    }
}
