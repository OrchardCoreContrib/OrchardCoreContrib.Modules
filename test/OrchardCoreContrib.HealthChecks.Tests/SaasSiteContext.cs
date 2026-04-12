using OrchardCoreContrib.Testing;

namespace OrchardCoreContrib.HealthChecks.Tests.Tests;

public class SaasSiteContext : SiteContextBase<OrchardCoreStartup>
{
    public SaasSiteContext() => Options.RecipeName = "SaaS";
}
