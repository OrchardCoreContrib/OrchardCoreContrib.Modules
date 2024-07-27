using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;
using OrchardCoreContrib.ContentLocalization.Services;
using OrchardCoreContrib.ContentLocalization.Transliteration;

namespace OrchardCoreContrib.ContentLocalization.Liquid;

public class ArabToLatFilter(ITransliterationService transliterationService) : ILiquidFilter
{
    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var text = input.ToStringValue();
        return new StringValue(transliterationService.Transliterate(TransliterateScript.Arabic, text));
    }
}