using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Yahoo.Drivers;
using OrchardCoreContrib.Email.Yahoo.ViewModels;

namespace OrchardCoreContrib.Email.Yahoo.Controllers;

public class AdminController(
    IHtmlLocalizer<AdminController> H,
    IAuthorizationService authorizationService,
    INotifier notifier,
    ISmtpService smtpService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!await authorizationService.AuthorizeAsync(User, YahooPermissions.ManageYahooSettings))
        {
            return Forbid();
        }

        return View();
    }

    [HttpPost, ActionName(nameof(Index))]
    public async Task<IActionResult> IndexPost(YahooSettingsViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, YahooPermissions.ManageYahooSettings))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            var message = CreateMessageFromViewModel(model);

            var result = await smtpService.SendAsync(message);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("*", error.ToString());
                }
            }
            else
            {
                await notifier.SuccessAsync(H["Message sent successfully"]);

                return Redirect(Url.Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = YahooSettingsDisplayDriver.GroupId }));
            }
        }

        return View(model);
    }

    private MailMessage CreateMessageFromViewModel(YahooSettingsViewModel testSettings)
    {
        var message = new MailMessage
        {
            To = testSettings.To,
            Bcc = testSettings.Bcc,
            Cc = testSettings.Cc,
            ReplyTo = testSettings.ReplyTo
        };

        if (!string.IsNullOrWhiteSpace(testSettings.Sender))
        {
            message.Sender = testSettings.Sender;
        }

        if (!string.IsNullOrWhiteSpace(testSettings.Subject))
        {
            message.Subject = testSettings.Subject;
        }

        if (!string.IsNullOrWhiteSpace(testSettings.Body))
        {
            message.Body = testSettings.Body;
        }

        return message;
    }
}
