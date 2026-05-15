using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.ResourceManagement;
using OrchardCoreContrib.CloudflareTurnstile.Configuration;

namespace OrchardCoreContrib.CloudflareTurnstile.Services;

public sealed class TurnstileShape(IResourceManager resourceManager, IOptions<TurnstileOptions> options) : IShapeAttributeProvider
{
    private readonly TurnstileOptions options = options.Value;

    [Shape]
    public async Task<IHtmlContent> Turnstile()
    {
        var script = new TagBuilder("script");
        script.MergeAttribute("src", Constants.TurnstileScriptUri);

        resourceManager.RegisterHeadScript(script);

        var div = new TagBuilder("div");
        div.AddCssClass("cf-turnstile");
        div.MergeAttribute("data-sitekey", options.SiteKey);
        div.MergeAttribute("data-theme", options.Theme);
        div.MergeAttribute("data-size", options.Size);

        return div;
    }
}
