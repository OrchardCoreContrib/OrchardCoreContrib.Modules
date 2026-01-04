using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;
using OrchardCoreContrib.Gravatar.Services;

namespace OrchardCoreContrib.Gravatar.Liquid;

public class GravatarFilter(IGravatarService gravatarService) : ILiquidFilter
{
    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var email = input.ToStringValue();

        if (input.IsNil())
        {
            return NilValue.Instance;
        }
        else
        {
            var size = GravatarConstants.DefaultSize;
            if (arguments.Count == 1)
            {
                size = (int)arguments["size"].ToNumberValue();
            }
            
            var gravatarUrl = gravatarService.GetAvatarUrl(email, size);

            return FluidValue.Create(gravatarUrl, context.Options);
        }
    }


}
