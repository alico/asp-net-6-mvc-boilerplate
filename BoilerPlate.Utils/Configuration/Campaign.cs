using System;

namespace BoilerPlate.Utils
{
    [Serializable]
    public class Campaign
    {
        public DateTime? SeasonStartDate { get; set; }
        public DateTime? SeasonEndDate { get; set; }
        public CampaignStatuses Status { get; set; }
        public Countries Country { get; set; }
    }
}
