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
using OrchardCore.Modules;
using OrchardCore.Users;

namespace OrchardCoreContrib.Users.Controllers
{
    [Authorize]
    [Feature("OrchardCoreContrib.Users.Impersonation")]
    public class ImpersonationController : Controller
    {
        private readonly SignInManager<IUser> _signInManager;
        private readonly UserManager<IUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly AdminOptions _adminOptions;
        private readonly ILogger _logger;
        private readonly IStringLocalizer S;

        public ImpersonationController(
            SignInManager<IUser> signInManager,
            UserManager<IUser> userManager,
            IAuthorizationService authorizationService,
            IOptions<AdminOptions> adminOptions,
            ILogger<ImpersonationController> logger,
            IStringLocalizer<ImpersonationController> stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _adminOptions = adminOptions.Value;
            _logger = logger;
            S = stringLocalizer;
        }

        public async Task<IActionResult> ImpersonateUser(string userId)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageImpersonationSettings))
            {
                return Forbid();
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

        public async Task<IActionResult> EndImpersonatation()
        {
            var isImpersonatingClaim = HttpContext.User.FindFirst(ClaimTypesExtended.IsImpersonating);
            if (isImpersonatingClaim == null || isImpersonatingClaim?.Value != "true")
            {
                return Forbid();
            }

            var impersonatorUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypesExtended.ImpersonatorNameIdentifier).Value;
            
            var impersonatedUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var impersonatedUser = await _userManager.FindByIdAsync(impersonatedUserId);
            var impersonatedUserPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);

            await _signInManager.SignOutAsync();

            var impersonaterUser = await _userManager.FindByIdAsync(impersonatorUserId);
            var impersonaterUserPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonaterUser);

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, impersonaterUserPrincipal);

            _logger.LogInformation(1, S["Swicthed back to the '{0}' user.", impersonaterUser.UserName]);

            return Redirect($"~/{_adminOptions.AdminUrlPrefix}");
        }
    }
}
