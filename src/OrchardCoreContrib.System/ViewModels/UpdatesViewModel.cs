using NuGet.Versioning;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.ViewModels;

public class UpdatesViewModel
{
    public SystemInformation SystemInformation { get; set; }

    public IEnumerable<NuGetVersion> Versions { get; set; }
}
