using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Localization.Json;

/// <summary>
/// Represents an <see cref="HtmlLocalizer"/> for JSON.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JsonHtmlLocalizer"/> class.
/// </remarks>
/// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
public class JsonHtmlLocalizer(IStringLocalizer localizer) : HtmlLocalizer(localizer)
{
    private readonly IStringLocalizer _localizer = localizer;

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name] => ToHtmlString(_localizer[name]);

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name, params object[] arguments]
        => ToHtmlString(_localizer[name], arguments);
}
