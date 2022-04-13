using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Web;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Web.Controllers
{
    public class HomeViewComponent : ViewComponent
    {
        private readonly ICampaignService _campaignService;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeViewComponent(ICampaignService campaignService, UserManager<ApplicationUser> userManager)
        {
            _campaignService = campaignService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var country = GetCountry();
            var campaign = await _campaignService.GetCampaign(country);
            switch (campaign.Status)
            {
                case CampaignStatuses.ClosePreSeason:
                    {
                        var model = new PreSeasonModel()
                        {
                            Country = country,
                            OpeningDate = new DateTimeOffset(campaign.SeasonStartDate.Value).ToUnixTimeMilliseconds().ToString()
                        };

                        return View("ClosePreSeason", model);
                    }
                case CampaignStatuses.ClosedForSeason:
                    return View("ClosedForSeason");
                default:
                    {
                        var model = new OpenModel()
                        {
                            Country = country,

                        };
                        if (User?.Identity?.IsAuthenticated == true)
                        {
                            var user = await _userManager.GetUserAsync(HttpContext.User);
                            model.UserName = user.FirstName;
                        }

                        return View("Open", model);
                    }
            }
        }

        private Countries GetCountry()
        {
            Countries country = Countries.None;
            var currentCulture = CultureInfo.CurrentUICulture;
            if (currentCulture is not null)
                country = EnumHelper.GetValueFromDescription<Countries>(currentCulture.Name);

            if (country == Countries.None)
                country = Countries.UK;

            return country;
        }
    }
}
