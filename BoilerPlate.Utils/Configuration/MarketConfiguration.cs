using System;
using System.Collections.Generic;
using System.Linq;

namespace BoilerPlate.Utils
{
    public class MarketConfiguration
    {
        public Dictionary<Countries, string> SupportedCultures { get; set; }
        public string DefaultCulture { get; set; }
        public string DefaultSlug { get; set; }
        public List<Countries> Countries { get; set; }
        public Dictionary<Countries, string> Hosts { get; set; }
        public Dictionary<Countries, string> CultureSlugs { get; set; }

        public string GetCultureByPath(string culture)
        {
            var country = SupportedCultures.Where(x => x.Value.Contains(culture, System.StringComparison.OrdinalIgnoreCase)).Select(x => x.Key).FirstOrDefault();
            if (country == default(Countries))
                country = Utils.Countries.UK;

            return country.GetDescription(); ;
        }

        public string GetCultureByHost(string host)
        {
            var country = Hosts.Where(x => x.Value.Contains(host, System.StringComparison.OrdinalIgnoreCase)).Select(x => x.Key).FirstOrDefault();
            if (country == default(Countries))
                country = Utils.Countries.UK;

            return country.GetDescription(); ;
        }
    }
}
