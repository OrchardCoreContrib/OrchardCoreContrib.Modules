using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Email;
using OrchardCoreContrib.Email.Gmail.Drivers;
using OrchardCoreContrib.Email.Gmail.ViewModels;

namespace OrchardCoreContrib.Email.Gmail.Controllers;

public class AdminController(
    IHtmlLocalizer<AdminController> H,
    IAuthorizationService authorizationService,
    INotifier notifier,
    ISmtpService smtpService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!await authorizationService.AuthorizeAsync(User, GmailPermissions.ManageGmailSettings))
        {
            return Forbid();
        }

        return View();
    }

    [HttpPost, ActionName(nameof(Index))]
    public async Task<IActionResult> IndexPost(GmailSettingsViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, GmailPermissions.ManageGmailSettings))
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

                return Redirect(Url.Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GmailSettingsDisplayDriver.GroupId }));
            }
        }

        return View(model);
    }

    private static MailMessage CreateMessageFromViewModel(GmailSettingsViewModel testSettings)
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
