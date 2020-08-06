using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Represents a shortcode attribute.
    /// </summary>
    public class ShortcodeAttribute : IHtmlContent
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

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(Name);
            writer.Write("=\"");

            if (Value != null)
            {
                encoder.Encode(writer, Value);
            }

            writer.Write("\"");
        }
    }
}
