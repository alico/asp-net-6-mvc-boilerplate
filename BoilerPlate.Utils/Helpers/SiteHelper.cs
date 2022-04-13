
using System.Globalization;

namespace BoilerPlate.Utils
{
    public static class SiteHelper
    {
        public static string GetHost(ApplicationSettings applicationSettings, Countries country)
        {
            return country switch
            {
                Countries.UK => applicationSettings.Hosts.UK,
                Countries.Au => applicationSettings.Hosts.Au,
                _ => throw new NotFoundException("Host couldn't be found"),
            };
        }


        public static Countries GetCountry()
        {
            var country = Countries.None;
            var currentCulture = CultureInfo.CurrentUICulture;
            if (currentCulture is not null)
                country = EnumHelper.GetValueFromDescription<Countries>(currentCulture.Name);

            if (country is Countries.None)
                country = Countries.UK;

            return country;
        }

        public static string GetGtmCode()
        {
            var country = GetCountry();
            return country switch
            {
                Countries.UK => "",
                Countries.Au => "",
                _ => "",
            };
        }

        public static string GetTitle()
        {
            return "BoilerPlate";
        }

        public static string GetDescription()
        {
            return "Lorem ipsum";
        }
    }
}
