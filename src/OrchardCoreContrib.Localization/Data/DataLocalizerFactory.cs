using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrchardCoreContrib.Localization.Data;

/// <summary>
/// Represents a factory for creating dynamic data localizers.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="DataLocalizerFactory"/>.
/// </remarks>
/// <param name="dataResourceManager">The <see cref="DataResourceManager"/>.</param>
/// <param name="requestLocalizationOptions">The <see cref="IOptions{RequestLocalizationOptions}"/>.</param>
/// <param name="logger">The <see cref="ILogger{DataLocalizerFactory}"/>.</param>
public class DataLocalizerFactory(
    DataResourceManager dataResourceManager,
    IOptions<RequestLocalizationOptions> requestLocalizationOptions,
    ILogger<DataLocalizerFactory> logger) : IDataLocalizerFactory
{
    private readonly bool _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;

    /// <inheritdoc/>
    public IDataLocalizer Create() => new DataLocalizer(dataResourceManager, _fallBackToParentCulture, logger);
}
