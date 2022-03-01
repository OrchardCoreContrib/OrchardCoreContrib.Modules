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
        private readonly DataResourceManager _dataResourceManager;
        private readonly bool _fallBackToParentCulture;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="DataLocalizerFactory"/>.
        /// </summary>
        /// <param name="dataResourceManager">The <see cref="DataResourceManager"/>.</param>
        /// <param name="requestLocalizationOptions">The <see cref="IOptions{RequestLocalizationOptions}"/>.</param>
        /// <param name="logger">The <see cref="ILogger{DataLocalizerFactory}"/>.</param>
        public DataLocalizerFactory(
            DataResourceManager dataResourceManager,
            IOptions<RequestLocalizationOptions> requestLocalizationOptions,
            ILogger<DataLocalizerFactory> logger)
        {
            _dataResourceManager = dataResourceManager;
            _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;
            _logger = logger;
        }

        /// <inheritdoc/>
        public IDataLocalizer Create() => new DataLocalizer(_dataResourceManager, _fallBackToParentCulture, _logger);
    }
}
