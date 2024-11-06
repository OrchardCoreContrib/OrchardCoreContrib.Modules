using Microsoft.Extensions.Logging;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Data;

/// <summary>
/// Represents a localizer for dynamic data.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="DataLocalizer"/>.
/// </remarks>
/// <param name="_dataResourceManager">The <see cref="DataResourceManager"/>.</param>
/// <param name="fallBackToParentCulture">Whether able to fallback to the parent culture.</param>
/// <param name="logger">The <see cref="Ilogger"/>.</param>
public class DataLocalizer(DataResourceManager dataResourceManager, bool fallBackToParentCulture, ILogger logger) : IDataLocalizer
{

    /// <inheritdoc/>
    public DataLocalizedString this[string name, string context]
    {
        get
        {
            Guard.ArgumentNotNullOrEmpty(name, nameof(name));
            Guard.ArgumentNotNullOrEmpty(context, nameof(context));

            var translation = GetTranslation(name, context, CultureInfo.CurrentUICulture);

            return new DataLocalizedString(name, context, translation ?? name, translation == null);
        }
    }

    /// <inheritdoc/>
    public DataLocalizedString this[string name, string context, params object[] arguments]
    {
        get
        {
            var translation = this[name, context];
            var localizedString = new DataLocalizedString(name, context, translation, translation.ResourceNotFound);
            var formatted = String.Format(localizedString.Value, arguments);

            return new DataLocalizedString(name, context, formatted, translation.ResourceNotFound);
        }
    }

    /// <inheritdoc/>
    public IEnumerable<DataLocalizedString> GetAllStrings(string context, bool includeParentCultures)
    {
        var culture = CultureInfo.CurrentUICulture;

        var translations = dataResourceManager.GetResources(culture, includeParentCultures);

        foreach (var translation in translations)
        {
            yield return new DataLocalizedString(translation.Key, context, translation.Value);
        }
    }

    private string GetTranslation(string name, string context, CultureInfo culture)
    {
        string translation = null;
        try
        {
            if (fallBackToParentCulture)
            {
                do
                {
                    translation = dataResourceManager.GetString(name, context, culture);

                    if (translation != null)
                    {
                        break;
                    }

                    culture = culture.Parent;
                }
                while (culture != CultureInfo.InvariantCulture);
            }
            else
            {
                translation = dataResourceManager.GetString(name, context);
            }
        }
        catch (PluralFormNotFoundException ex)
        {
            logger.LogWarning(ex.Message);
        }

        return translation;
    }
}
