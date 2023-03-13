using Microsoft.Extensions.Logging;
using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a localizer for dynamic data.
    /// </summary>
    public class DataLocalizer : IDataLocalizer
    {
        private readonly DataResourceManager _dataResourceManager;
        private readonly bool _fallBackToParentCulture;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="DataLocalizer"/>.
        /// </summary>
        /// <param name="_dataResourceManager">The <see cref="DataResourceManager"/>.</param>
        /// <param name="fallBackToParentCulture">Whether able to fallback to the parent culture.</param>
        /// <param name="logger">The <see cref="Ilogger"/>.</param>
        public DataLocalizer(DataResourceManager dataResourceManager, bool fallBackToParentCulture, ILogger logger)
        {
            _dataResourceManager = dataResourceManager;
            _fallBackToParentCulture = fallBackToParentCulture;
            _logger = logger;
        }

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

            var translations = _dataResourceManager.GetResources(culture, includeParentCultures);

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
                if (_fallBackToParentCulture)
                {
                    do
                    {
                        translation = _dataResourceManager.GetString(name, context, culture);

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
                    translation = _dataResourceManager.GetString(name, context);
                }
            }
            catch (PluralFormNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
            }

            return translation;
        }
    }
}
