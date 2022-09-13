using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;
using System;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents a <see cref="IStringLocalizerFactory"/> for XLIFF.
/// </summary>
public class XliffStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly ILocalizationManager _localizationManager;
    private readonly bool _fallBackToParentCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="XliffStringLocalizerFactory"/> class.
    /// </summary>
    /// <param name="localizationManager">The <see cref="ILocalizationManager"/>.</param>
    /// <param name="requestLocalizationOptions">The <see cref="IOptions{RequestLocalizationOptions}"/>.</param>
    public XliffStringLocalizerFactory(
        ILocalizationManager localizationManager,
        IOptions<RequestLocalizationOptions> requestLocalizationOptions)
    {
        _localizationManager = localizationManager;
        _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;
    }

    /// <inheritedoc />
    public IStringLocalizer Create(Type resourceSource)
        => new XliffStringLocalizer(_localizationManager, _fallBackToParentCulture);

    /// <inheritedoc />
    public IStringLocalizer Create(string baseName, string location)
        => new XliffStringLocalizer(_localizationManager, _fallBackToParentCulture);
}
