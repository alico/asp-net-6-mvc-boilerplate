using BoilerPlate.Bootstrapper;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using reCAPTCHA.AspNetCore.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BoilerPlate.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;


        public AccountController(IIdentityService identityService,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService,
            ILogger<AdminController> logger,
            ApplicationSettings applicationSettings,
            IDistributedCache cache,
            IEncryptionService encryptionService, IEmailService emailService) : base (userManager, campaignService, logger, applicationSettings, cache, encryptionService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("signup")]
        [CampaignSeason(CampaignStatuses.Open)]
        public async Task<IActionResult> SignUp(string returnUrl)
        {
            var model = new SignUpModel()
            {
                Country = MarketConfiguration.Country,
                TermsConditionsHTML = "Lorem Ipsum",
                ReturnUrl = returnUrl
            };
            //await _identityService.Logout();
            return View("SignUp", model);
        }

        [HttpPost]
        [Route("signup")]
        [CampaignSeason(CampaignStatuses.Open)]
        [ValidateAntiForgeryToken]
        //[ValidateRecaptcha(0.5)]
        public async Task<IActionResult> SignUp(SignUpModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Tuple<IdentityResult, ApplicationUser> result = null;
                var user = await _identityService.FindByEmailAsync(model.Email);

                if (user != null)
                    ModelState.AddModelError("", "You have already registered.");
                else
                {
                    result = await CreateUser(model);
                    if (result.Item1.Succeeded)
                    {
                        user = result.Item2;
                        var confirmationToken = await _identityService.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = $"{UrlHelper.GenerateAccountConfirmationLink(MarketConfiguration)}{user.Id}/{HttpUtility.UrlEncode(confirmationToken)}";

                        await _emailService.SendUserConfirmationEmail(MarketConfiguration, user, confirmationLink);

                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);

                        return RedirectToAction(nameof(SignIn));
                    }
                    else
                        foreach (var error in result.Item1.Errors)
                            ModelState.AddModelError("", error.Description);
                }
            }

            model.TermsConditionsHTML = "Lorem Ipsum";
            model.ReturnUrl = returnUrl;

            return View("SignUp", model);
        }

        [Route("sign-in")]
        public IActionResult SignIn(string returnUrl)
        {
            return View();
        }


        [HttpPost]
        [Route("sign-in")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _identityService.SignIn(model.UserName, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains(ApplicationUserRoles.ApplicationUser.ToString()))
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        await _identityService.Logout();
                }
            }

            ModelState.AddModelError("", "Invalid Login Attempt");
            return View();
        }

        [Route("confirm-email/{userId}")]
        public async Task<IActionResult> ConfirmEmail(string userId, string confirmationToken, string returnUrl = "/")
        {
            var user = await _identityService.FindByUserId(userId);
            if (user != null)
            {
                var result = await _identityService.ConfirmEmailAsync(user, confirmationToken);
                if (result.Succeeded)
                {
                    await _identityService.SignIn(user);
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return LocalRedirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Confirmation token has expired. Please sign up again.");
            return RedirectToAction(nameof(SignUp));
        }

        [HttpGet]
        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _identityService.Logout();
            return RedirectToAction("Index", "Home");
        }

        private async Task<Tuple<IdentityResult, ApplicationUser>> CreateUser(SignUpModel model)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = false,
                FirstName = model.FirstName,
                CountryId = (int)MarketConfiguration.Country,
                IsApprovedTermsConditions = model.TermsConditions,
                IsOptInMarketing = model.MarketingOptIn,
                IsOverThanAgeLimit = model.AgeCheck,
                ExpireDate = DateTime.Now.AddYears(1),
                CreationDate = DateTime.Now,
                LastmodifyDate = DateTime.Now
            };

            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
            var result = await _identityService.Register(user);

            if (result.Succeeded)
            {
                if (user == null)
                    user = await _identityService.FindByEmailAsync(model.Email);

                await _identityService.AddToRoleAsync(user, ApplicationUserRoles.ApplicationUser.ToString());
            }

            return new Tuple<IdentityResult, ApplicationUser>(result, user);
        }
    }
}
