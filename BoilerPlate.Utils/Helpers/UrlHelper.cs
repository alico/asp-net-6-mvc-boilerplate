
using System.Globalization;

namespace BoilerPlate.Utils
{
    public static class UrlHelper
    {
        public static Countries GetCountryByCode(string code)
        {
            return EnumHelper.GetValueFromDescription<Countries>(code);
        }

        public static string GenerateAccountConfirmationLink(Market site)
        {
            return $"{site.Host}/confirm-email";
        }
    }
}
