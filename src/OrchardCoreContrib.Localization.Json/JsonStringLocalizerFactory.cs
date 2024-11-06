using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;
using System;

namespace OrchardCoreContrib.Localization.Json;

/// <summary>
/// Represents a <see cref="IStringLocalizerFactory"/> for JSON.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JsonStringLocalizerFactory"/> class.
/// </remarks>
/// <param name="localizationManager"></param>
/// <param name="requestLocalizationOptions"></param>
/// <param name="logger"></param>
public class JsonStringLocalizerFactory(
    ILocalizationManager localizationManager,
    IOptions<RequestLocalizationOptions> requestLocalizationOptions,
    ILogger<JsonStringLocalizerFactory> logger) : IStringLocalizerFactory
{
    private readonly bool _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;

    /// <inheritedoc />
    public IStringLocalizer Create(Type resourceSource)
        => new JsonStringLocalizer(localizationManager, _fallBackToParentCulture, logger);

    /// <inheritedoc />
    public IStringLocalizer Create(string baseName, string location)
        => new JsonStringLocalizer(localizationManager, _fallBackToParentCulture, logger);
}
