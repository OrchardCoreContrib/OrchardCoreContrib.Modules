using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;
using OrchardCoreContrib.Avatars;

namespace OrchardCoreContrib.Gravatar.Liquid;

public class GravatarFilter(IAvatarService avatarService) : ILiquidFilter
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
            
            var gravatarUrl = avatarService.GetAvatar(new AvatarContext { Email = email }, size);

            return FluidValue.Create(gravatarUrl, context.Options);
        }
    }


}
