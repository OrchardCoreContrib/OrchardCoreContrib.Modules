using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Email;
using OrchardCoreContrib.Email.SendGrid.Drivers;
using OrchardCoreContrib.Email.SendGrid.ViewModels;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.SendGrid.Controllers;

public class AdminController(
    IHtmlLocalizer<AdminController> H,
    IAuthorizationService authorizationService,
    INotifier notifier,
    ISmtpService smtpService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!await authorizationService.AuthorizeAsync(User, SendGridPermissions.ManageSendGridSettings))
        {
            return Forbid();
        }

        return View();
    }

    [HttpPost, ActionName(nameof(Index))]
    public async Task<IActionResult> IndexPost(SendGridSettingsViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, SendGridPermissions.ManageSendGridSettings))
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

                return Redirect(Url.Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = SendGridSettingsDisplayDriver.GroupId }));
            }
        }

        return View(model);
    }

    private static MailMessage CreateMessageFromViewModel(SendGridSettingsViewModel testSettings)
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
