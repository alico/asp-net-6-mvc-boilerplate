using BoilerPlate.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BoilerPlate.Bootstrapper
{
    public static class MarketConfigurationExtension
    {
        public static IServiceCollection AddMarkets(this IServiceCollection services, ApplicationSettings applicationSettings, out MarketConfiguration marketConfiguration)
        {
            var countries = EnumHelper.GetEnumList<Countries>();
            marketConfiguration = new MarketConfiguration()
            {
                Countries = countries,
                CultureSlugs = countries.Select(x => new { Key = x, Value = x.GetDescription()?.Split("-").LastOrDefault() ?? "" }).ToDictionary(x => x.Key, x => x.Value),
                DefaultCulture = Countries.UK.GetDescription(),
                DefaultSlug = Countries.UK.GetDescription()?.Split("-").LastOrDefault() ?? "",
                SupportedCultures = countries.Select(x => new { Key = x, Value = x.GetDescription() }).ToDictionary(x => x.Key, x => x.Value),
                Hosts = GetHosts(applicationSettings)
            };

            services.AddSingleton(marketConfiguration);
            return services;
        }

        private static Dictionary<Countries, string> GetHosts(ApplicationSettings applicationSettings)
        {
            Dictionary<Countries, string> hosts = new Dictionary<Countries, string>();

            PropertyInfo[] properties = typeof(ApplicationSettings).GetProperties();
            foreach (PropertyInfo property in applicationSettings.Hosts.GetType().GetProperties())
            {
                Countries country = Countries.None;
                Enum.TryParse(property.Name, out country);

                if (country != Countries.None)
                {
                    var value = property.GetValue(applicationSettings.Hosts)?.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(value))
                    {
                        hosts.Add(country, value);
                    }
                }
            }
            return hosts;
        }
    }
}
