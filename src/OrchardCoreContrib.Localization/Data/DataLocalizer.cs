using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public DataLocalizer(
            DataResourceManager dataResourceManager,
            bool fallBackToParentCulture,
            ILogger logger)
        {
            _dataResourceManager = dataResourceManager;
            _fallBackToParentCulture = fallBackToParentCulture;
            _logger = logger;
        }

        /// <inheritdoc/>
        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var translation = GetTranslation(name, CultureInfo.CurrentUICulture);

                return new LocalizedString(name, translation ?? name, translation == null);
            }
        }

        /// <inheritdoc/>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var (translation, argumentsWithCount) = GetTranslation(name, arguments);
                var formatted = String.Format(translation.Value, argumentsWithCount);

                return new LocalizedString(name, formatted, translation.ResourceNotFound);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentUICulture;

            return includeParentCultures
                ? GetAllStringsFromCultureHierarchy(culture)
                : GetAllStrings(culture);
        }

        /// <inheritdoc/>
        public IStringLocalizer WithCulture(CultureInfo culture) => this;

        /// <inheritdoc/>
        public (LocalizedString, object[]) GetTranslation(string name, params object[] arguments)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var translation = this[name];

            return (new LocalizedString(name, translation, translation.ResourceNotFound), arguments);
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

        private IEnumerable<LocalizedString> GetAllStrings(CultureInfo culture)
        {
            var translations = _dataResourceManager.GetResources(culture);

            foreach (var translation in translations)
            {
                yield return new LocalizedString(translation.Key, translation.Value.FirstOrDefault());
            }
        }

        private string GetTranslation(string name, CultureInfo culture)
        {
            string translation = null;
            try
            {
                if (_fallBackToParentCulture)
                {
                    do
                    {
                        translation = _dataResourceManager.GetString(name, culture);

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
                    translation = _dataResourceManager.GetString(name);
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
