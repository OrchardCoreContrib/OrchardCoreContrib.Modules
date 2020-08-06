using System;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Provides an <see cref="IShortcode"/>'s target.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ShortcodeTargetAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeAttribute"/>.
        /// </summary>
        /// <param name="name">The shortcode name.</param>
        public ShortcodeTargetAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the shortcode name
        /// </summary>
        public string Name { get; }
    }
}
