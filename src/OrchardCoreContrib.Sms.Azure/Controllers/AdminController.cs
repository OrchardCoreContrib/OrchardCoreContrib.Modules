using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCoreContrib.Sms.Azure.ViewModels;

namespace OrchardCoreContrib.Sms.Azure.Controllers;

public class AdminController : Controller
{
    private readonly ISmsService _smsService;
    private readonly IAuthorizationService _authorizationService;
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer H;

    public AdminController(
        ISmsService smsService,
        IAuthorizationService authorizationService,
        INotifier notifier,
        IHtmlLocalizer<AdminController> htmlLocalizer)
    {
        _smsService = smsService;
        _authorizationService = authorizationService;
        _notifier = notifier;
        H = htmlLocalizer;
    }

    public async Task<IActionResult> Index()
    {
        if (!await _authorizationService.AuthorizeAsync(User, AzureSmsPermissions.ManageSettings))
        {
            return Forbid();
        }

        return View(new AzureSmsSettingsViewModel());
    }

    [ValidateAntiForgeryToken]
    [HttpPost, ActionName(nameof(Index))]
    public async Task<IActionResult> IndexPost(AzureSmsSettingsViewModel model)
    {
        if (!await _authorizationService.AuthorizeAsync(User, AzureSmsPermissions.ManageSettings))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            var result = await _smsService.SendAsync(model.PhoneNumber, model.Message);

            if (result.Succeeded)
            {
                await _notifier.SuccessAsync(H["The test SMS message has been successfully sent."]);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _notifier.ErrorAsync(H["The test SMS message failed to send."]);
            }
        }

        return View(model);
    }
}
