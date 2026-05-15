using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;
using OrchardCoreContrib.ContentLocalization.Services;
using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace OrchardCoreContrib.ContentLocalization.Liquid;

/// <summary>
/// Provides a Liquid filter that transliterates Cyrillic text to Latin script using the specified transliteration
/// service.
/// </summary>
/// <param name="transliterationService">The transliteration service used to convert Cyrillic text to Latin script. Cannot be null.</param>
public class CyrToLatFilter(ITransliterationService transliterationService) : ILiquidFilter
{
    /// <inheritdoc/>
    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var text = input.ToStringValue();

        return new StringValue(transliterationService.Transliterate(TransliterateScript.Cyrillic, text));
    }
}