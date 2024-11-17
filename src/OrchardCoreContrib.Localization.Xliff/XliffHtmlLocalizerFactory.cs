using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents an <see cref="IHtmlLocalizerFactory"/> for XLIFF.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="XliffHtmlLocalizerFactory"/> class.
/// </remarks>
/// <param name="stringLocalizerFactory">The <see cref="IStringLocalizerFactory"/>.</param>
public class XliffHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory) : IHtmlLocalizerFactory
{
    /// <inheritdocs />
    public IHtmlLocalizer Create(string baseName, string location)
        => new XliffHtmlLocalizer(stringLocalizerFactory.Create(baseName, location));

    /// <inheritdocs />
    public IHtmlLocalizer Create(Type resourceSource)
        => new XliffHtmlLocalizer(stringLocalizerFactory.Create(resourceSource));
}
