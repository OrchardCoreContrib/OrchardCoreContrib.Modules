using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.System.Services;
using OrchardCoreContrib.System.ViewModels;

namespace OrchardCoreContrib.System.Controllers;

public class AdminController : Controller
{
    private readonly SystemInformation _systemInformation;
    private readonly IShellHost _shellHost;
    private readonly IShellFeaturesManager _shellFeaturesManager;
    private readonly ISystemUpdateService _systemUpdateService;

    public AdminController(
        SystemInformation systemInformation,
        IShellHost shellHost,
        IShellFeaturesManager shellFeaturesManager,
        ISystemUpdateService systemUpdateService)
    {
        _systemInformation = systemInformation;
        _shellHost = shellHost;
        _shellFeaturesManager = shellFeaturesManager;
        _systemUpdateService = systemUpdateService;
    }

    public async Task<ActionResult> About() => View(new AboutViewModel
    {
        SystemInformation = _systemInformation,
        Tenants = _shellHost.GetAllSettings(),
        Features = await _shellFeaturesManager.GetEnabledFeaturesAsync()
    });

    public async Task<ActionResult> Updates()
    {
        var updates = await _systemUpdateService.GetUpdatesAsync();

        return View(new UpdatesViewModel
        {
            SystemInformation = _systemInformation,
            Updates = updates,
        });
    }
}
