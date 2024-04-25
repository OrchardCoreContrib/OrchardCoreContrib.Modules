using Microsoft.AspNetCore.Html;
using OrchardCoreContrib.Infrastructure;
using System;
using System.IO;
using System.Text.Encodings.Web;

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
        /// <param name="attributes">The shortcode attributes.</param>
        public ShortcodeOutput(ShortcodeAttributes attributes)
        {
            Guard.ArgumentNotNull(attributes, nameof(attributes));

            Attributes = attributes;
        }

        /// <summary>
        /// Gets a shortcode attributes list.
        /// </summary>
        public ShortcodeAttributes Attributes { get; }

        /// <summary>
        /// Gets or sets the shortcode content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the tag name that used in the generated HTML.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the element syntax in the generated HTML.
        /// </summary>
        public TagMode TagMode { get; set; }

        /// <summary>
        /// Changes the shortcode output to generate nothing.
        /// </summary>
        public void SuppressOutput()
        {
            TagName = null;
            Content = null;
        }

        /// <inheritdoc/>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            Guard.ArgumentNotNull(writer, nameof(writer));
            Guard.ArgumentNotNull(encoder, nameof(encoder));

            var isTagNameNullOrWhitespace = string.IsNullOrWhiteSpace(TagName);
            if (!isTagNameNullOrWhitespace)
            {
                writer.Write("<");
                writer.Write(TagName);

                foreach (var attribute in Attributes)
                {
                    writer.Write(" ");
                    attribute.WriteTo(writer, encoder);
                }

                if (TagMode == TagMode.SelfClosing)
                {
                    writer.Write(" /");
                }

                writer.Write(">");
            }

            if (isTagNameNullOrWhitespace || TagMode == TagMode.StartTagAndEndTag)
            {
                encoder.Encode(writer, Content);
            }

            if (!isTagNameNullOrWhitespace && TagMode == TagMode.StartTagAndEndTag)
            {
                writer.Write("</");
                writer.Write(TagName);
                writer.Write(">");
            }
        }
    }
}
