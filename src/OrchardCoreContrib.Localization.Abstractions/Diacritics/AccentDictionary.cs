using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Localization.Diacritics
{
    /// <summary>
    /// Represents a dictionary accents.
    /// </summary>
    public class AccentDictionary : IEnumerable<AccentDictionaryRecord>
    {
        private readonly IList<AccentDictionaryRecord> _mapping = new List<AccentDictionaryRecord>();

        /// <summary>
        /// Initializes a new instance of a <see cref="AccentDictionary"/>.
        /// </summary>
        /// <param name="culture">The culture to be associated with the dictionary.</param>
        public AccentDictionary(string culture)
        {
            Culture = culture;
        }

        /// <summary>
        /// Gets the dictionay culture.
        /// </summary>
        public string Culture { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[char key] => _mapping.SingleOrDefault(m => m.Key == key).Value ?? null;

        /// <summary>
        /// Gets the counts for the dictionary records.
        /// </summary>
        public int Count => _mapping.Count;

        /// <summary>
        /// Adds an item to the dictionary.
        /// </summary>
        /// <param name="key">The key for the added item.</param>
        /// <param name="value">The value for the added item.</param>
        public void Add(char key, string value)
        {
            _mapping.Add(new AccentDictionaryRecord(key, value));
        }

        /// <summary>
        /// Adds an item to the dictionary.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public void Add(AccentDictionaryRecord item)
        {
            _mapping.Add(item);
        }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        public void Clear()
        {
            _mapping.Clear();
        }

        /// <summary>
        /// Checks whether a dictionary contains a given key or not.
        /// </summary>
        /// <param name="key">The key to be checked.</param>
        public bool Contains(char key) => _mapping.Any(m => m.Key == key);

        /// <summary>
        /// Removes a given key from a dictioanry.
        /// </summary>
        /// <param name="key">The key of the record to be removed.</param>
        public void Remove(char key)
        {
            if (Contains(key))
            {
                var record = _mapping.Single(m => m.Key == key);

                _mapping.Remove(record);
            }
        }

        /// <inheritdoc/>
        public IEnumerator<AccentDictionaryRecord> GetEnumerator()
        {
            foreach (var entry in _mapping)
            {
                yield return new AccentDictionaryRecord(entry.Key, entry.Value);
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
