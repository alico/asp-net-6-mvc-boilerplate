using BoilerPlate.Data.Entities;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service
{
    public class CampaignService : ICampaignService
    {
        private readonly ISettingService _settingService;
        private readonly ICacheService _cacheService;

        public CampaignService(ISettingService settingService,
                               ICacheService cacheService)
        {
            _cacheService = cacheService;
            _settingService = settingService;
        }

        public async Task<Campaign> GetCampaign(Countries country, bool purgeCache = false)
        {
            var cacheKey = $"{Constants.CampaignCacheKey}-{country}";
            var campaignSettings = await _cacheService.Get(cacheKey, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(1),
                FetchCampaignDTO(country, purgeCache),
                purgeCache);

            return campaignSettings;
        }

        public async Task<Campaign> GetCampaign(bool purgeCache = false)
        {
            var country = SiteHelper.GetCountry();
            var cacheKey = $"{Constants.CampaignCacheKey}-{country}";

            var campaignSettings = await _cacheService.Get(cacheKey, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(1),
               FetchCampaignDTO(country, purgeCache),
               purgeCache);

            return campaignSettings;
        }

        public async Task<List<Campaign>> GetCampaigns(bool purgeCache = false)
        {
            var countries = EnumHelper.GetEnumList<Countries>();
            List<Campaign> model = new List<Campaign>();

            foreach (var country in countries)
            {
                var campaign = await FetchCampaignDTO(country, purgeCache);
                model.Add(campaign);
            }

            return model;
        }

        private async Task<Campaign> FetchCampaignDTO(Countries country, bool purgeCache = false)
        {
            var campaignSettings = await _settingService.GetByGroupId(country, Constants.CampaignSettings, purgeCache);
            var now = DateTime.Now;

            Campaign campaign = new Campaign()
            {
                Country = country,
                Status = CampaignStatuses.ClosedForSeason
            };

            if (campaignSettings is not null && campaignSettings.Count > 0)
            {
                campaign.SeasonEndDate = campaignSettings.Where(x => x.Key == Constants.SeasonEndDateKey).Select(x => x.Value).FirstOrDefault().TryConvertToDateTime();
                campaign.SeasonStartDate = campaignSettings.Where(x => x.Key == Constants.SeasonStartDate).Select(x => x.Value).FirstOrDefault().TryConvertToDateTime();

                if (!campaign.SeasonEndDate.HasValue || campaign.SeasonEndDate <= now)
                    campaign.Status = CampaignStatuses.ClosedForSeason;

                else if (!campaign.SeasonStartDate.HasValue || campaign.SeasonStartDate >= now)
                    campaign.Status = CampaignStatuses.ClosePreSeason;
                else
                    campaign.Status = CampaignStatuses.Open;
            }

            return campaign;
        }
    }
}
