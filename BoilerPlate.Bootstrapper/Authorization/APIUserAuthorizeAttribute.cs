using BoilerPlate.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;

namespace BoilerPlate.Bootstrapper
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class APIUserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (ApplicationUser)context.HttpContext.Items["User"];
            if (user is not null)
            {
                //Override Current culture with user's
                if (user.Country is not Countries.None)
                {
                    CultureInfo.CurrentCulture = new CultureInfo(user.Country.GetDescription(), false);
                    CultureInfo.CurrentUICulture = new CultureInfo(user.Country.GetDescription(), false);
                }
            }
            else
                throw new AuthenticationFailedException();
        }
    }
}
