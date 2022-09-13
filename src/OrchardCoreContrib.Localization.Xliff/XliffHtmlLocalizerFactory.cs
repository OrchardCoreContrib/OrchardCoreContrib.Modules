using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents an <see cref="IHtmlLocalizerFactory"/> for XLIFF.
/// </summary>
public class XliffHtmlLocalizerFactory : IHtmlLocalizerFactory
{
    private readonly IStringLocalizerFactory _stringLocalizerFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="XliffHtmlLocalizerFactory"/> class.
    /// </summary>
    /// <param name="stringLocalizerFactory">The <see cref="IStringLocalizerFactory"/>.</param>
    public XliffHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory)
    {
        _stringLocalizerFactory = stringLocalizerFactory;
    }

    /// <inheritdocs />
    public IHtmlLocalizer Create(string baseName, string location)
        => new XliffHtmlLocalizer(_stringLocalizerFactory.Create(baseName, location));

    /// <inheritdocs />
    public IHtmlLocalizer Create(Type resourceSource)
        => new XliffHtmlLocalizer(_stringLocalizerFactory.Create(resourceSource));
}
