using System.Globalization;
using System.Text;

namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a utility to remove a diacritics.
/// </summary>
/// <remarks>
/// Initializes a new instance of a <see cref="DiacriticsRemover"/>.
/// </remarks>
/// <param name="diacriticsLookup">The <see cref="IDiacriticsRemover"/>.</param>
public class DiacriticsRemover(IDiacriticsLookup diacriticsLookup) : IDiacriticsRemover
{

    /// <inheritdoc/>
    public string Remove(string source)
    {
        var normalizedText = source.Normalize(NormalizationForm.FormKD);
        var result = new StringBuilder();

        for (var i = 0; i < normalizedText.Length; i++)
        {
            var currentChar = normalizedText[i];
            if (CharUnicodeInfo.GetUnicodeCategory(currentChar) == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            var currentCultureName = CultureInfo.CurrentUICulture.Name;
            if (diacriticsLookup.Contains(currentCultureName))
            {
                var mappedChars = diacriticsLookup[currentCultureName].Mapping[currentChar];

                if (mappedChars != null)
                {
                    result.Append(mappedChars);
                    continue;
                }
            }

            result.Append(currentChar);
        }

        return result.ToString().Normalize(NormalizationForm.FormC);
    }
}
