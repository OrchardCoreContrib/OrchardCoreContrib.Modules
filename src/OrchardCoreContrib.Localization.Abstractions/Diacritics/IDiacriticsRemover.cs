namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a contract for removing diacritics.
/// </summary>
public interface IDiacriticsRemover
{
    /// <summary>
    /// Removes a diacritics from a given string.
    /// </summary>
    /// <param name="source">The string to remove the diacritcs from.</param>
    string Remove(string source);
}
