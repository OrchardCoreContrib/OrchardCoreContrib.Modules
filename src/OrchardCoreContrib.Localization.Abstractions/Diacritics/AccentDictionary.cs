using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public class AccentDictionary : IEnumerable<AccentDictionaryRecord>
    {
        private readonly IList<AccentDictionaryRecord> _mapping = new List<AccentDictionaryRecord>();

        public AccentDictionary(string culture)
        {
            Culture = culture;
        }

        public string Culture { get; }

        public int Count => _mapping.Count;

        public void Add(char key, string value)
        {
            _mapping.Add(new AccentDictionaryRecord(key, value));
        }

        public void Add(AccentDictionaryRecord item)
        {
            _mapping.Add(item);
        }

        public void Clear()
        {
            _mapping.Clear();
        }

        public bool Contains(char key) => _mapping.Any(m => m.Key == key);

        public void Remove(char key)
        {
            var record = _mapping.SingleOrDefault(m => m.Key == key);

            if (record != null)
            {
                _mapping.Remove(record);
            }
        }

        public IEnumerator<AccentDictionaryRecord> GetEnumerator()
        {
            foreach (var entry in _mapping)
            {
                yield return new AccentDictionaryRecord(entry.Key, entry.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
