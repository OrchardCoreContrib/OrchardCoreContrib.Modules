namespace OrchardCoreContrib.DataLocalization.Models;

/// <summary>
/// Represents a translation.
/// </summary>
public class Translation
{
    /// <summary>
    /// Gets the translation context.
    /// </summary>
    public string Context { get; set; }

    /// <summary>
    /// Gets the translation key.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets the translation value.
    /// </summary>
    public string Value { get; set; }
}
