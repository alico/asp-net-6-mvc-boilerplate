using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using BoilerPlate.Utils;
using System.Linq;
using BoilerPlate.Service.Contract;

namespace BoilerPlate.Bootstrapper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CampaignSeasonAttribute : Attribute, IActionFilter
    {
        public CampaignStatuses[] CampaignStatuses { get; set; }

        public CampaignSeasonAttribute(params CampaignStatuses[] campaignStatuses)
        {
            CampaignStatuses = campaignStatuses;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CampaignStatuses is not null)
            {
                var campaignService = filterContext.HttpContext.RequestServices.GetService<ICampaignService>();
                var campaign = campaignService.GetCampaign().Result;

                if (!CampaignStatuses.Any(x => x == campaign.Status))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Index"
                    }));
                }
            }  
        }
    }
}
