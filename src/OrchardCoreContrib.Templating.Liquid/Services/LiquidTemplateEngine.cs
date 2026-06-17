using Fluid;
using Microsoft.Extensions.Localization;
using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Templating.Liquid.Services;

/// <summary>
/// Represents a template engine that uses the Liquid templating language.
/// </summary>
public class LiquidTemplateEngine(IStringLocalizer<LiquidTemplateEngine> S) : ITemplateEngine
{
    private readonly FluidParser _fluidParser = new();

    /// <inheritdoc/>
    public async Task<Result<string>> RenderAsync(string template, TemplateContext context)
    {
        Guard.ArgumentNotNullOrEmpty(template);
        Guard.ArgumentNotNull(context);

        if (!_fluidParser.TryParse(template, out var parsedTemplate, out var errors))
        {
            return Result.Failed<string>(S["Liquid parsing failed: {0}", string.Join(", ", errors)]);
        }

        var fluidTemplateContext = new Fluid.TemplateContext(context.Model);

        try
        {
            var rendered = await parsedTemplate.RenderAsync(fluidTemplateContext);

            return Result.Success(rendered);
        }
        catch (Exception ex)
        {
            return Result.Failed<string>(S["Rendering liquid template failed: {0}", ex.Message]);
        }
    }
}
