using System;
using System.Collections;
using System.Collections.Generic;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Represents a list of <see cref="ShortcodeAttribute"/>.
    /// </summary>
    public class ShortcodeAttributes : IList<ShortcodeAttribute>
    {
        private readonly List<ShortcodeAttribute> _shortcodeAttributes;

        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeAttributes"/>.
        /// </summary>
        public ShortcodeAttributes()
        {
            _shortcodeAttributes = new List<ShortcodeAttribute>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeAttributes"/>.
        /// </summary>
        public ShortcodeAttributes(IEnumerable<ShortcodeAttribute> attributes)
        {
            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            _shortcodeAttributes = new List<ShortcodeAttribute>(attributes);
        }

        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeAttributes"/>.
        /// </summary>
        public ShortcodeAttributes(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            _shortcodeAttributes = new List<ShortcodeAttribute>();

            foreach (var attribute in attributes)
            {
                Add(attribute.Key, attribute.Value);
            }
        }

        /// <inheritdoc />
        public ShortcodeAttribute this[int index]
        { 
            get => _shortcodeAttributes[index];
            set => _shortcodeAttributes[index] = value;
        }

        /// <inheritdoc />
        public int Count => _shortcodeAttributes.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds attribute with a given name and value.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        public void Add(string name, string value)
        {
            var attribute = new ShortcodeAttribute(name, value);
            Add(attribute);
        }

        /// <inheritdoc />
        public void Add(ShortcodeAttribute item)
        {
            _shortcodeAttributes.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _shortcodeAttributes.Clear();
        }

        /// <summary>
        /// Checks whether the attribute with the given name is exist.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A <see cref="bool"/> indicates whether the attributes exists or not.</returns>
        public bool Contains(string name)
            => _shortcodeAttributes.Find(a => a.Name == name) != null;

        /// <inheritdoc />
        public bool Contains(ShortcodeAttribute item) => _shortcodeAttributes.Contains(item);

        /// <inheritdoc />
        public void CopyTo(ShortcodeAttribute[] array, int arrayIndex)
            => _shortcodeAttributes.CopyTo(array, arrayIndex);

        /// <summary>
        /// Gets attribute value.
        /// </summary>
        /// <param name="name">The attribute name to be looking for.</param>
        /// <returns>A <see cref="string"/> represents the attribute value.</returns>
        public string Get(string name)
        {
            var attribute = _shortcodeAttributes.Find(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return attribute.Value;
        }

        /// <inheritdoc />
        public IEnumerator<ShortcodeAttribute> GetEnumerator() => _shortcodeAttributes.GetEnumerator();

        /// <inheritdoc />
        public int IndexOf(ShortcodeAttribute item) => _shortcodeAttributes.IndexOf(item);

        /// <inheritdoc />
        public void Insert(int index, ShortcodeAttribute item)
        {
            _shortcodeAttributes.Insert(index, item);
        }

        /// <inheritdoc />
        public bool Remove(ShortcodeAttribute item) => _shortcodeAttributes.Remove(item);

        public void RemoveAt(int index)
        {
            _shortcodeAttributes.RemoveAt(index);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Sets an attribute value.
        /// </summary>
        /// <param name="name">The attribute name to be set.</param>
        /// <param name="value">The new value.</param>
        public void Set(string name, string value)
        {
            var attribute = new ShortcodeAttribute(name, value);
            Set(attribute);
        }

        /// <summary>
        /// Sets an attribute.
        /// </summary>
        /// <param name="attribute">The <see cref="ShortcodeAttribute"/> to be set.</param>
        public void Set(ShortcodeAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            var index = IndexOf(attribute);
            if (index > -1)
            {
                this[index] = attribute;
            }
        }
    }
}
