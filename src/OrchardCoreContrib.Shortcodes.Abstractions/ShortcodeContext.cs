using System;
using System.Collections.Generic;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Contains information related to the execution of <see cref="IShortcode"/>s.
    /// </summary>
    public class ShortcodeContext
    {
        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeContext"/>.
        /// </summary>
        /// <param name="shortcodeName">The shortcode name.</param>
        /// <param name="attributes">The shortcode attributes.</param>
        public ShortcodeContext(string shortcodeName, IDictionary<string, string> attributes)
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
        public IDictionary<string, string> Attributes { get; }

        /// <summary>
        /// Gets a shortcode name.
        /// </summary>
        public string ShortcodeName { get; }
    }
}
