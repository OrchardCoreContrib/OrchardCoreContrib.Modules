using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents a <see cref="IStringLocalizerFactory"/> for XLIFF.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="XliffStringLocalizerFactory"/> class.
/// </remarks>
/// <param name="localizationManager">The <see cref="ILocalizationManager"/>.</param>
/// <param name="requestLocalizationOptions">The <see cref="IOptions{RequestLocalizationOptions}"/>.</param>
public class XliffStringLocalizerFactory(
    ILocalizationManager localizationManager,
    IOptions<RequestLocalizationOptions> requestLocalizationOptions) : IStringLocalizerFactory
{
    private readonly bool _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;

    /// <inheritedoc />
    public IStringLocalizer Create(Type resourceSource)
        => new XliffStringLocalizer(localizationManager, _fallBackToParentCulture);

    /// <inheritedoc />
    public IStringLocalizer Create(string baseName, string location)
        => new XliffStringLocalizer(localizationManager, _fallBackToParentCulture);
}
