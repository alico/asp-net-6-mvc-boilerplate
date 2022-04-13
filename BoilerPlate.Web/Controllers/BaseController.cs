using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : Controller
    {
        private Market _market;
        private ApplicationUser _user;
        private Campaign _campaign;

        protected readonly ILogger<BaseController> _logger;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IDistributedCache _cache;
        protected readonly ApplicationSettings _applicationSettings;
        protected readonly IEncryptionService _encryptionService;
        protected readonly ICampaignService _campaignService;

        public BaseController(UserManager<ApplicationUser> userManager,
            ICampaignService campaignService,
            ILogger<BaseController> logger,
            ApplicationSettings applicationSettings,
            IDistributedCache cache,
            IEncryptionService encryptionService) =>
            (_logger, _applicationSettings, _userManager, _campaignService, _cache, _encryptionService) =
            (logger, applicationSettings, userManager, campaignService, cache, encryptionService);


        protected ApplicationUser ApplicationUser
        {
            get
            {
                if (_user == null)
                    InitializeUser();
                return _user;
            }
        }

        protected virtual Market MarketConfiguration
        {
            get
            {
                if (_market == null)
                    InitializeMarket();
                return _market;
            }
        }

        protected Campaign Campaign
        {
            get
            {
                if (_campaign == null)
                    InitializeCampaign();
                return _campaign;
            }
        }

        private void InitializeUser()
        {
            if (HttpContext?.User?.Identity?.IsAuthenticated ?? false)
                _user = _userManager.GetUserAsync(HttpContext.User).Result;
        }

        private void InitializeMarket()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            if (currentCulture is not null)
            {
                var country = EnumHelper.GetValueFromDescription<Countries>(currentCulture.Name);
                _market = new Market()
                {
                    Campaign = this.Campaign,
                    Country = country,
                    Host = GetHost(country)
                };
            }
        }

        private void InitializeCampaign()
        {
            var currentCulture = CultureInfo.CurrentUICulture;
            if (currentCulture is not null)
            {
                var country = EnumHelper.GetValueFromDescription<Countries>(currentCulture.Name);
                _campaign = _campaignService.GetCampaign(country).Result;
            }
        }

        protected string GetHost(Countries country)
        {
            return SiteHelper.GetHost(_applicationSettings, country);
        }

        protected string GetCookie(string key)
        {
            return Request.Cookies[key];
        }

        public void SetCookie(string key, string value, TimeSpan expireDate)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.Add(expireDate);
            Response.Cookies.Append(key, value, option);
        }
    }
}