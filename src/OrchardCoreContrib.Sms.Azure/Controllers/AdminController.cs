using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCoreContrib.Sms.Azure.ViewModels;
using OrchardCoreContrib.Validation;

namespace OrchardCoreContrib.Sms.Azure.Controllers;

public class AdminController(
    IPhoneNumberValidator phoneNumberValidator,
    ISmsService smsService,
    IAuthorizationService authorizationService,
    INotifier notifier,
    IHtmlLocalizer<AdminController> H) : Controller
{
    public async Task<IActionResult> Index()
    {
        if (!await authorizationService.AuthorizeAsync(User, AzureSmsPermissions.ManageAzureSmsSettings))
        {
            return Forbid();
        }

        return View(new AzureSmsSettingsViewModel());
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Index(AzureSmsSettingsViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, AzureSmsPermissions.ManageAzureSmsSettings))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            if (!phoneNumberValidator.IsValid(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(AzureSmsSettingsViewModel.PhoneNumber), H["Invalid Phone Number."].Value);
            }

            var result = await smsService.SendAsync(model.PhoneNumber, model.Message);

            if (result.Succeeded)
            {
                await notifier.SuccessAsync(H["The test SMS message has been successfully sent."]);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await notifier.ErrorAsync(H["The test SMS message failed to send."]);
            }
        }

        return View(model);
    }
}
