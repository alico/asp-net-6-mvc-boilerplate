using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public class SettingsController : BaseController
    {
        private readonly ISettingService _settingService;
        public SettingsController(UserManager<ApplicationUser> userManager,
            ApplicationSettings applicationSettings,
            ISettingService settingService,
             ICampaignService campaignService,
             IDistributedCache cache,
             IEncryptionService encryptionService,
             ILogger<SettingsController> logger) : base(userManager, campaignService, logger, applicationSettings, cache, encryptionService)
        {
            _settingService = settingService;
        }

        [Route("settings/{countryCode}")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create(short countryCode)
        {
            ViewData["Countries"] = GetCountries();
            var model = await GetSettings((Countries)countryCode);

            return View(model);
        }

        [HttpPost]
        [Route("settings/{countryCode}")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create(short countryCode, SettingsModel settingsModel)
        {
            ViewData["Countries"] = GetCountries();
            settingsModel.Country = (Countries)countryCode;
            if (ModelState.IsValid)
            {
                await _settingService.Upsert(settingsModel);
            }
            else
                ModelState.AddModelError("", "Invalid configuration");

            //To refresh the cache
            await _campaignService.GetCampaign((Countries)countryCode, true);

            return View(settingsModel);
        }

        private async Task<SettingsModel> GetSettings(Countries country)
        {
            var settings = await _settingService.GetAll(country);
            return new SettingsModel()
            {
                APISecret = settings.Where(x => x.Key == Constants.ClientSecret).Select(x => x.Value).FirstOrDefault(),
                StartDate = settings.Where(x => x.Key == Constants.SeasonStartDate).Select(x => Convert.ToDateTime(x.Value)).FirstOrDefault(),
                EndDate = settings.Where(x => x.Key == Constants.SeasonEndDateKey).Select(x => Convert.ToDateTime(x.Value)).FirstOrDefault(),
                Country = country,
            };
        }

        private List<KeyValueModel> GetCountries()
        {
            return Enum.GetValues(typeof(Countries)).Cast<Countries>().Where(x => x != Countries.None).Select(v => new KeyValueModel() { Key = v.ToString(), Value = ((short)v).ToString() }).ToList();
        }
    }
}
