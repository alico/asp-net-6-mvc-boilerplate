using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Contract
{
    public interface IIdentityService
    {
        #region Customer Identity Services
        Task<IdentityResult> Register(ApplicationUser user);

        Task<IdentityResult> Update(ApplicationUser user);

        Task<IdentityResult> UpdateConsumerId(string id, string consumerId);
        Task SignIn(ApplicationUser user);
        Task<SignInResult> SignIn(string userName, string password, bool rememberMe);
        bool IsSignedIn(ClaimsPrincipal principal);
        Task Logout();
        Task<SignInResult> Login(string email, string password, bool rememberMe);

        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<string> GenerateSignInLink(Market site, ApplicationUser applicationUser, string returnUrl = null);
        bool Any(string email);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindActiveUserByEmailAsync(string email);
        Task<ApplicationUser> FindByUserId(string id);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> CreateUserAccountIfNotExits(Market market, string email, string firstName);
        #endregion

        #region Seed Users
        Task CreateRoles();
        Task CreateAdminUsers();
        Task CreateAPIUsers();
        #endregion

        #region Customer Identity Services
        Task<ApplicationUser> AuthenticateAPIUser(string token);
        #endregion
    }
}
