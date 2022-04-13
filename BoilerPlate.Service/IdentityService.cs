using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using BoilerPlate.Web.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Web;

namespace BoilerPlate.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISettingService _settingService;
        private readonly IEncryptionService _encryptionService;
        private readonly ICampaignService _campaignService;

        public IdentityService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ISettingService settingService,
            ICampaignService campaignService,
            IEncryptionService encryptionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settingService = settingService;
            _encryptionService = encryptionService;
            _campaignService = campaignService;
            _roleManager = roleManager;
        }

        #region Customer Identity Service
        public async Task<IdentityResult> Register(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task SignIn(ApplicationUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<SignInResult> SignIn(string userName, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
        }

        public async Task<IdentityResult> Update(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateConsumerId(string id, string consumerId)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.UpdateAsync(user);
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }

        public async Task<bool> IsEmailConfirmed(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> Login(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User Not Found");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GenerateSignInLink(Market site, ApplicationUser applicationUser, string returnUrl = null)
        {
            var authenticationToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            var returnUrlParam = string.Empty;

            if (!string.IsNullOrEmpty(returnUrl))
                returnUrlParam = $"&returnUrl={returnUrl}";

            return $"{site.Host}/sign-in/{applicationUser.Id}?auth={HttpUtility.UrlEncode(authenticationToken)}{returnUrlParam}";
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public bool Any(string email)
        {
            return _userManager.Users.Any(x => x.Email == email);
        }

        public async Task<ApplicationUser> FindByUserId(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> FindActiveUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && DateTime.Now <= user.ExpireDate)
                return user;

            return null;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
        #endregion

        #region API User Identity Services
        public async Task<ApplicationUser> AuthenticateAPIUser(string token)
        {
            var clientSecret = await _settingService.GetByKeyValue(Constants.ClientSecret, token);
            if (clientSecret != null && clientSecret.Value == token)
            {
                var country = (Countries)clientSecret.CountryId;
                var user = await _userManager.FindByEmailAsync($"{country}@boilerplate.com");

                if (user is not null)
                    user.Country = country;

                return user;
            }

            return null;
        }

        public async Task<ApplicationUser> CreateUserAccountIfNotExits(Market market, string email, string firstName)
        {
            var user = await FindByEmailAsync(email);
            if (user is null)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var password = string.Empty;

                user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = firstName,
                    IsOverThanAgeLimit = true,
                    IsApprovedTermsConditions = true,
                    IsOptInMarketing = true,
                    CountryId = (int)market.Country,
                    ExpireDate = DateTime.Now.AddYears(1),
                    CreationDate = DateTime.Now,
                    LastmodifyDate = DateTime.Now
                };

                user.PasswordHash = passwordHasher.HashPassword(user, string.Empty);

                var createUserTask = await Register(user);
                if (createUserTask.Succeeded)
                    await AddToRoleAsync(user, ApplicationUserRoles.ApplicationUser.ToString());
            }

            return user;
        }

        #endregion

        #region Seed Users
        public async Task CreateAdminUsers()
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var password = string.Empty;

            var user = new ApplicationUser()
            {
                UserName = $"admin",
                Email = $"admin@boilerplate.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = $"admin",
                IsOverThanAgeLimit = true,
                IsApprovedTermsConditions = true,
                IsOptInMarketing = false,
                ExpireDate = DateTime.Now.AddYears(1),
                CreationDate = DateTime.Now,
                LastmodifyDate = DateTime.Now
            };

            var isUserExist = await FindByEmailAsync(user.Email) == null;
            if (isUserExist)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, Constants.AdminUserPassword);

                var createUserTask = await Register(user);
                if (createUserTask.Succeeded)
                    await AddToRoleAsync(user, ApplicationUserRoles.Admin.ToString());
            }
        }

        public async Task CreateAPIUsers()
        {
            var countries = Enum.GetValues(typeof(Countries)).Cast<Countries>().ToList();
            var userIsExist = false;
            var password = string.Empty;
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            foreach (var country in countries)
            {
                userIsExist = await FindByEmailAsync($"{country}@boilerplate.com") != null;
                var user = new ApplicationUser()
                {
                    UserName = $"{country}APIUser",
                    Email = $"{country}@boilerplate.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = $"{country} API User",
                    IsOverThanAgeLimit = true,
                    IsApprovedTermsConditions = true,
                    IsOptInMarketing = false,
                    ExpireDate = DateTime.Now.AddYears(1),
                    CreationDate = DateTime.Now,
                    LastmodifyDate = DateTime.Now,
                };

                if (!userIsExist)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, Constants.APIUserPassword);

                    var createUserTask = await Register(user);
                    if (createUserTask.Succeeded)
                        await AddToRoleAsync(user, ApplicationUserRoles.APIUser.ToString());
                }
            }
            #endregion
        }

        public async Task CreateRoles()
        {
            string[] roleNames = {
                                    ApplicationUserRoles.ApplicationUser.ToString(),
                                   ApplicationUserRoles.Admin.ToString(),
                                   ApplicationUserRoles.APIUser.ToString()};

            foreach (var roleName in roleNames)
            {
                var roleExistTask = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExistTask)
                    await _roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
    }
}
