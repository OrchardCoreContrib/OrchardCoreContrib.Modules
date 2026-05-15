using Microsoft.AspNetCore.Mvc;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.System.Services;
using OrchardCoreContrib.System.ViewModels;

namespace OrchardCoreContrib.System.Controllers;

public class AdminController(
    SystemInformation systemInformation,
    IShellHost shellHost,
    IShellFeaturesManager shellFeaturesManager,
    ISystemUpdateService systemUpdateService) : Controller
{
    public async Task<ActionResult> About() => View(new AboutViewModel
    {
        SystemInformation = systemInformation,
        Tenants = shellHost.GetAllSettings(),
        Features = await shellFeaturesManager.GetEnabledFeaturesAsync()
    });

    public async Task<ActionResult> Updates() => View(new UpdatesViewModel
    {
        SystemInformation = systemInformation,
        Updates = await systemUpdateService.GetUpdatesAsync(),
    });
}
