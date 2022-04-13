using System;

namespace BoilerPlate.Utils
{
    public class Market
    {
        public Countries Country { get; set; }
        public string Host { get; set; }
        public Campaign Campaign { get; set; }
        public string CampaignStartDate { get { return DateHelper.GetLocalDateFormatted(Campaign.SeasonStartDate.Value, Country); } }
    }
}
