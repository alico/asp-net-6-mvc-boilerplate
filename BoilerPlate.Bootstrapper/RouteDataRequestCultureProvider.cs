using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BoilerPlate.Bootstrapper
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;
        public ApplicationSettings ApplicationSettings;
        public MarketConfiguration MarketConfiguration;


        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var culture = string.Empty;
            var twoLetterCultureName = string.Empty;
            if (httpContext.Request.QueryString.HasValue)
            {
                var queryString = HttpUtility.ParseQueryString(httpContext.Request.QueryString.Value);
                twoLetterCultureName = queryString["culture"];
            }

            if (string.IsNullOrEmpty(twoLetterCultureName))
                twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();

            culture = MarketConfiguration.GetCultureByPath(twoLetterCultureName);

            var providerResultCulture = new ProviderCultureResult(culture, culture);
            return Task.FromResult(providerResultCulture);
        }
    }
}
