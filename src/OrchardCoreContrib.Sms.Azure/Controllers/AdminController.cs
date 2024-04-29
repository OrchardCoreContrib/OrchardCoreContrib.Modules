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
    private readonly IPhoneNumberValidator _phoneNumberValidator = phoneNumberValidator;
    private readonly ISmsService _smsService = smsService;
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly INotifier _notifier = notifier;

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

        if (!_phoneNumberValidator.IsValid(model.PhoneNumber))
        {
            ModelState.AddModelError(nameof(AzureSmsSettingsViewModel.PhoneNumber), H["Invalid Phone Number."].Value);
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
