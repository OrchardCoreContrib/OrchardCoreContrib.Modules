namespace OrchardCoreContrib.Shortcodes;

/// <summary>
/// Provides an <see cref="IShortcode"/>'s target.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="ShortcodeAttribute"/>.
/// </remarks>
/// <param name="name">The shortcode name.</param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class ShortcodeTargetAttribute(string name) : Attribute
{

    /// <summary>
    /// Gets the shortcode name
    /// </summary>
    public string Name { get; } = name;
}
