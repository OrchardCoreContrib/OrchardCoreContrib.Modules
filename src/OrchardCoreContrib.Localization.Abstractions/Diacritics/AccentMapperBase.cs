using System.Globalization;

namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a base class for accent mapper.
/// </summary>
public abstract class AccentMapperBase : IAccentMapper
{
    /// <summary>
    /// Initializes a new instance of a <see cref="AccentMapperBase"/>.
    /// </summary>
    protected AccentMapperBase() => Mapping = new AccentDictionary(Culture.Name);

    /// <inheritdoc/>
    public abstract CultureInfo Culture { get; }

    /// <inheritdoc/>
    public AccentDictionary Mapping { get; }
}
