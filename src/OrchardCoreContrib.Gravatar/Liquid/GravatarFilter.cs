using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;
using OrchardCoreContrib.Gravatar.Services;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Gravatar.Liquid;

public class GravatarFilter : ILiquidFilter
{
    private readonly IGravatarService _gravatarService;

    public GravatarFilter(IGravatarService gravatarService)
    {
        _gravatarService = gravatarService;
    }

    public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, LiquidTemplateContext context)
    {
        var email = input.ToStringValue();

        if (input.IsNil())
        {
            return NilValue.Empty;
        }
        else
        {
            var gravatarUrl = _gravatarService.GetAvatarUrl(email);

            return FluidValue.Create(gravatarUrl, context.Options);
        }
    }


}
