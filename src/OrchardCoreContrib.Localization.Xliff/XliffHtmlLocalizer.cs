using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents an <see cref="HtmlLocalizer"/> for XLIFF.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="XliffHtmlLocalizer"/> class.
/// </remarks>
/// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
/// <remarks>
/// Initializes a new instance of the <see cref="XliffHtmlLocalizer"/> class.
/// </remarks>
/// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
public class XliffHtmlLocalizer(IStringLocalizer localizer) : HtmlLocalizer(localizer)
{
    private readonly IStringLocalizer _localizer = localizer;

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name] => ToHtmlString(_localizer[name]);

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name, params object[] arguments]
        => ToHtmlString(_localizer[name], arguments);
}
