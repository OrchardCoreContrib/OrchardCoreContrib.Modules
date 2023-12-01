using Fluid.Values;

namespace OrchardCoreContrib.Liquid;

public class Environment
{
    public BooleanValue IsDevelopment { get; set; }

    public BooleanValue IsStaging { get; set; }

    public BooleanValue IsProduction { get; set; }
}
