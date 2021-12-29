using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public class DiacriticsLookup : IDiacriticsLookup
    {
        private readonly IDictionary<string, IAccentMapper> _accentsDiacriticsMapper;

        public DiacriticsLookup(IEnumerable<IAccentMapper> accentMappers)
        {
            _accentsDiacriticsMapper = new Dictionary<string, IAccentMapper>();

            foreach (var mapper in accentMappers)
            {
                _accentsDiacriticsMapper.TryAdd(mapper.Culture.Name, mapper);
            }
        }

        public IAccentMapper this[string culture] => _accentsDiacriticsMapper[culture];

        public int Count => _accentsDiacriticsMapper.Count;

        public bool Contains(string culture) => _accentsDiacriticsMapper.ContainsKey(culture);
    }
}
