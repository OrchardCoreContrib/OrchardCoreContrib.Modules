using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a diacritics lookup table.
/// </summary>
public class DiacriticsLookup : IDiacriticsLookup
{
    private readonly IDictionary<string, IAccentMapper> _accentsDiacriticsMapper;

    /// <summary>
    /// Initializes a new instance of a <see cref="DiacriticsLookup"/>.
    /// </summary>
    /// <param name="accentMappers">The <see cref="IEnumerable{IAccentMapper}"/>.</param>
    public DiacriticsLookup(IEnumerable<IAccentMapper> accentMappers)
    {
        _accentsDiacriticsMapper = new Dictionary<string, IAccentMapper>();

        foreach (var mapper in accentMappers)
        {
            _accentsDiacriticsMapper.TryAdd(mapper.Culture.Name, mapper);
        }
    }

    /// <inheritdoc/>
    public IAccentMapper this[string culture] => _accentsDiacriticsMapper[culture];

    /// <inheritdoc/>
    public int Count => _accentsDiacriticsMapper.Count;

    /// <inheritdoc/>
    public bool Contains(string culture) => _accentsDiacriticsMapper.ContainsKey(culture);
}
