using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Represents an output of an <see cref="IShortcode"/>.
    /// </summary>
    public class ShortcodeOutput : IHtmlContent
    {
        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeOutput"/>.
        /// </summary>
        /// <param name="shortcodeName">The shortcode name.</param>
        /// <param name="attributes">The shortcode attributes.</param>
        public ShortcodeOutput(string shortcodeName, ShortcodeAttributes attributes)
        {
            if (shortcodeName is null)
            {
                throw new ArgumentNullException(nameof(shortcodeName));
            }

            if (attributes is null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            ShortcodeName = shortcodeName;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets a shortcode attributes list.
        /// </summary>
        public ShortcodeAttributes Attributes { get; }

        /// <summary>
        /// Gets a shortcode content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets a shortcode name.
        /// </summary>
        public string ShortcodeName { get; set; }

        /// <summary>
        /// Changes the shortcode output to generate nothing.
        /// </summary>
        public void SuppressOutput()
        {
            ShortcodeName = null;
            Content = null;
        }

        /// <inheritdoc/>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            writer.Write(Content);
        }
    }
}
