using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a contract for mapped accent.
/// </summary>
public interface IAccentMapper
{
    /// <summary>
    /// Gets the culture.
    /// </summary>
    CultureInfo Culture { get; }

    /// <summary>
    /// Gets the accent dictionary.
    /// </summary>
    AccentDictionary Mapping { get; }
}
