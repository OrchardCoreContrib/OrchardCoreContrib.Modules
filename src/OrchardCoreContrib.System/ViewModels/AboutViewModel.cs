using OrchardCore.Environment.Extensions.Features;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.ViewModels;

public class AboutViewModel
{
    public SystemInformation SystemInformation { get; set; }

    public IEnumerable<ShellSettings> Tenants { get; set; }

    public IEnumerable<IFeatureInfo> Features { get; set; }
}
