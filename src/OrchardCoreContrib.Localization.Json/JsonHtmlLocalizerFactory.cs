using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace OrchardCoreContrib.Localization.Json;

/// <summary>
/// Represents an <see cref="IHtmlLocalizerFactory"/> for JSON.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JsonHtmlLocalizerFactory"/> class.
/// </remarks>
/// <param name="stringLocalizerFactory">The <see cref="IStringLocalizerFactory"/>.</param>
public class JsonHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory) : IHtmlLocalizerFactory
{
    /// <inheritdocs />
    public IHtmlLocalizer Create(string baseName, string location)
        => new JsonHtmlLocalizer(stringLocalizerFactory.Create(baseName, location));

    /// <inheritdocs />
    public IHtmlLocalizer Create(Type resourceSource)
        => new JsonHtmlLocalizer(stringLocalizerFactory.Create(resourceSource));
}
