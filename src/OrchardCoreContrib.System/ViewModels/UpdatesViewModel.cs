using OrchardCoreContrib.System.Models;
using OrchardCoreContrib.System.Services;

namespace OrchardCoreContrib.System.ViewModels;

public class UpdatesViewModel
{
    public SystemInformation SystemInformation { get; set; }

    public IEnumerable<SystemUpdate> Updates { get; set; }
}
