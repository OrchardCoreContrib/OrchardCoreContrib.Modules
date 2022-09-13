using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace OrchardCoreContrib.Localization.Xliff;

/// <summary>
/// Represents an <see cref="HtmlLocalizer"/> for XLIFF.
/// </summary>
public class XliffHtmlLocalizer : HtmlLocalizer
{
    private readonly IStringLocalizer _localizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="XliffHtmlLocalizer"/> class.
    /// </summary>
    /// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
    public XliffHtmlLocalizer(IStringLocalizer localizer) : base(localizer)
    {
        _localizer = localizer;
    }

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name] => ToHtmlString(_localizer[name]);

    /// <inheritdocs />
    public override LocalizedHtmlString this[string name, params object[] arguments]
        => ToHtmlString(_localizer[name], arguments);
}
