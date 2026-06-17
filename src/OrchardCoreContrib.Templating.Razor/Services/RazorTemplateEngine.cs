using Microsoft.Extensions.Localization;
using OrchardCoreContrib.Infrastructure;
using RazorLight;

namespace OrchardCoreContrib.Templating.Razor.Services;

/// <summary>
/// Represents a template engine that uses the Razor templating language.
/// </summary>
public class RazorTemplateEngine(IStringLocalizer<RazorTemplateEngine> S) : ITemplateEngine
{
    private readonly RazorLightEngine _engine = new RazorLightEngineBuilder()
        .UseMemoryCachingProvider()
        .Build();

    /// <inheritdoc />
    public async Task<Result<string>> RenderAsync(string template, TemplateContext context)
    {
        Guard.ArgumentNotNullOrEmpty(template, nameof(template));
        Guard.ArgumentNotNull(context, nameof(context));

        try
        {
            var result = await _engine.CompileRenderStringAsync(Guid.NewGuid().ToString(), template, context.Model);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Failed<string>(S["Rendering razor template failed: {0}", ex.Message]);
        }
    }
}
