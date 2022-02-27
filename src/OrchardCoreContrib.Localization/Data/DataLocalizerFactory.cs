using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a factory for creating dynamic data localizers.
    /// </summary>
    public class DataLocalizerFactory : IDataLocalizerFactory
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly bool _fallBackToParentCulture;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="DataLocalizerFactory"/>.
        /// </summary>
        /// <param name="localizationManager">The <see cref="ILocalizationManager"/>.</param>
        /// <param name="requestLocalizationOptions">The <see cref="IOptions{RequestLocalizationOptions}"/>.</param>
        /// <param name="logger">The <see cref="ILogger{DataLocalizerFactory}"/>.</param>
        public DataLocalizerFactory(
            ILocalizationManager localizationManager,
            IOptions<RequestLocalizationOptions> requestLocalizationOptions,
            ILogger<DataLocalizerFactory> logger)
        {
            _localizationManager = localizationManager;
            _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IDataLocalizer Create() => new DataLocalizer(_localizationManager, _fallBackToParentCulture, _logger);
    }
}
