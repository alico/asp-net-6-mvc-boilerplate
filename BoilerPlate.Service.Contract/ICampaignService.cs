using BoilerPlate.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Contract
{
    public interface ICampaignService
    {
        Task<Campaign> GetCampaign(Countries country, bool purgeCache = false);
        Task<List<Campaign>> GetCampaigns(bool purgeCache = false);
        Task<Campaign> GetCampaign(bool purgeCache = false);
    }
}
