using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Entities;
using OrchardCore.Modules;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCoreContrib.Users.Models;

namespace OrchardCoreContrib.Users.Controllers
{
    [Authorize]
    [Feature("OrchardCore.Users.Impersonation")]
    public class ImpersonationController : Controller
    {
        private readonly SignInManager<IUser> _signInManager;
        private readonly UserManager<IUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISiteService _siteService;
        private readonly AdminOptions _adminOptions;
        private readonly ILogger _logger;
        private readonly IStringLocalizer S;

        public ImpersonationController(
            SignInManager<IUser> signInManager,
            UserManager<IUser> userManager,
            IAuthorizationService authorizationService,
            ISiteService siteService,
            IOptions<AdminOptions> adminOptions,
            ILogger<ImpersonationController> logger,
            IStringLocalizer<ImpersonationController> stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _siteService = siteService;
            _adminOptions = adminOptions.Value;
            _logger = logger;
            S = stringLocalizer;
        }

        public async Task<IActionResult> ImpersonateUser(string userId)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageUsers))
            {
                return Forbid();
            }

            if (!(await _siteService.GetSiteSettingsAsync()).As<ImpersonationSettings>().EnableImpersonation)
            {
                return NotFound();
            }

            var currentUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var impersonatedUser = await _userManager.FindByIdAsync(userId);
            var impersonatedUserPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);

            impersonatedUserPrincipal.Identities.First().AddClaim(new Claim(ClaimTypesExtended.ImpersonatorNameIdentifier, currentUserId));
            impersonatedUserPrincipal.Identities.First().AddClaim(new Claim(ClaimTypesExtended.IsImpersonating, "true"));

            await _signInManager.SignOutAsync();
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, impersonatedUserPrincipal);

            _logger.LogInformation(1, S["User '{0}' logged as '{1}'.", User.Identity.Name, impersonatedUser.UserName]);

            return Redirect($"~/{_adminOptions.AdminUrlPrefix}");
        }
    }
}
