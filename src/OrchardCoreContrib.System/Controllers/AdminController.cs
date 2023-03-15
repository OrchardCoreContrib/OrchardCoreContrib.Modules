using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.System.Services;
using OrchardCoreContrib.System.ViewModels;

namespace OrchardCoreContrib.System.Controllers;

public class AdminController : Controller
{
    private readonly SystemInformation _systemInformation;
    private readonly IShellHost _shellHost;
    private readonly IShellFeaturesManager _shellFeaturesManager;

    public AdminController(
        SystemInformation systemInformation,
        IShellHost shellHost,
        IShellFeaturesManager shellFeaturesManager)
    {
        _systemInformation = systemInformation;
        _shellHost = shellHost;
        _shellFeaturesManager = shellFeaturesManager;
    }

    public async Task<ActionResult> About() => View(new AboutViewModel
    {
        SystemInformation = _systemInformation,
        Tenants = _shellHost.GetAllSettings(),
        Features = await _shellFeaturesManager.GetEnabledFeaturesAsync()
    });

    public async Task<ActionResult> Updates()
    {
        var repository = Repository.Factory.GetCoreV3(SystemUpdatesConstants.NugetPackageSource);
        
        var resource = await repository.GetResourceAsync<FindPackageByIdResource>();

        var versions = await resource.GetAllVersionsAsync(
            SystemUpdatesConstants.OrchardCorePackageId,
            new SourceCacheContext(),
            NullLogger.Instance,
            CancellationToken.None);

        return View(new UpdatesViewModel
        {
            SystemInformation = _systemInformation,
            Versions = versions.Reverse(),
        });
    }
}
