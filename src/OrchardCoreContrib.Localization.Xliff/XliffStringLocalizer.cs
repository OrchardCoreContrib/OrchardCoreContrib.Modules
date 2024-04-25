using Microsoft.Extensions.Localization;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents <see cref="IStringLocalizer"/> for XLIFF.
/// </summary>
public class XliffStringLocalizer : IStringLocalizer
{
    private readonly ILocalizationManager _localizationManager;
    private readonly bool _fallBackToParentCulture;

    /// <summary>
    /// Creates a new instance of <see cref="PortableObjectStringLocalizer"/>.
    /// </summary>
    /// <param name="localizationManager">The <see cref="ILocalizationManager"/>.</param>
    /// <param name="fallBackToParentCulture">Whether fallback to the parent culture.</param>
    public XliffStringLocalizer(
        ILocalizationManager localizationManager,
        bool fallBackToParentCulture)
    {
        _localizationManager = localizationManager;
        _fallBackToParentCulture = fallBackToParentCulture;
    }

    /// <inheritdocs />
    public LocalizedString this[string name]
    {
        get
        {
            Guard.ArgumentNotNull(name, nameof(name));

            var translation = GetTranslation(name, CultureInfo.CurrentUICulture);

            return new LocalizedString(name, translation ?? name, translation == null);
        }
    }

    /// <inheritdocs />
    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var translation = this[name];
            var formatted = string.Format(translation.Value, arguments);

            return new LocalizedString(name, formatted, translation.ResourceNotFound);
        }
    }

    /// <inheritdocs />
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var culture = CultureInfo.CurrentUICulture;

        return includeParentCultures
            ? GetAllStringsFromCultureHierarchy(culture)
            : GetAllStrings(culture);
    }

    private IEnumerable<LocalizedString> GetAllStrings(CultureInfo culture)
    {
        var dictionary = _localizationManager.GetDictionary(culture);

        foreach (var translation in dictionary.Translations)
        {
            yield return new LocalizedString(translation.Key, translation.Value.FirstOrDefault());
        }
    }

    private IEnumerable<LocalizedString> GetAllStringsFromCultureHierarchy(CultureInfo culture)
    {
        var currentCulture = culture;
        var allLocalizedStrings = new List<LocalizedString>();

        do
        {
            var localizedStrings = GetAllStrings(currentCulture);

            if (localizedStrings != null)
            {
                foreach (var localizedString in localizedStrings)
                {
                    if (!allLocalizedStrings.Any(ls => ls.Name == localizedString.Name))
                    {
                        allLocalizedStrings.Add(localizedString);
                    }
                }
            }

            currentCulture = currentCulture.Parent;
        } while (currentCulture != currentCulture.Parent);

        return allLocalizedStrings;
    }

    private string GetTranslation(string name, CultureInfo culture)
    {
        string translation = null;
        if (_fallBackToParentCulture)
        {
            do
            {
                if (ExtractTranslation() != null)
                {
                    break;
                }

                culture = culture.Parent;
            }
            while (culture != CultureInfo.InvariantCulture);
        }
        else
        {
            ExtractTranslation();
        }

        string ExtractTranslation()
        {
            var dictionary = _localizationManager.GetDictionary(culture);

            if (dictionary != null)
            {
                if (translation == null)
                {
                    var key = new CultureDictionaryRecordKey(name);
                    translation = dictionary[key];
                }
            }

            return translation;
        }

        return translation;
    }
}
