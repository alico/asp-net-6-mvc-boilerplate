using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AdminController(IIdentityService identityService,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService,
            ILogger<AdminController> logger,
            ApplicationSettings applicationSettings,
            IDistributedCache cache,
            IEncryptionService encryptionService) : base (userManager, campaignService, logger, applicationSettings, cache, encryptionService)
        {
            _identityService = identityService;
        }


        [Route("admin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("admin")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _identityService.SignIn(model.UserName, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains(ApplicationUserRoles.Admin.ToString()))
                        return RedirectToAction("Create", "Settings", new { countryCode = (short)Countries.UK });
                    else
                        await _identityService.Logout();
                }
            }

            ModelState.AddModelError("", "Invalid Login Attempt");
            return View();
        }
    }
}
