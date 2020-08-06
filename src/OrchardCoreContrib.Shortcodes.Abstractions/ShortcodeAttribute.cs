using System;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Represents a shortcode attribute.
    /// </summary>
    public class ShortcodeAttribute
    {
        /// <summary>
        /// Create a new instance of <see cref="ShortcodeAttribute"/>.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        public ShortcodeAttribute(string name) : this(name, null)
        {

        }

        /// <summary>
        /// Create a new instance of <see cref="ShortcodeAttribute"/>.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        public ShortcodeAttribute(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the attribute.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        /// <remarks><see cref="Name"/> is compared case-insensitively.</remarks>
        public bool Equals(ShortcodeAttribute other)
            => other != null && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && Equals(Value, other.Value);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as ShortcodeAttribute;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Name, Value);
    }
}
