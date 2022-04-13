using BoilerPlate.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace BoilerPlate.Bootstrapper
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey("culture"))
                return false;

            var cultureSlugs = EnumHelper.GetEnumList<Countries>().Select(x => new { Key = x, Value = x.GetDescription() }).ToDictionary(x => x.Key, x => x.Value);
            var culture = values["culture"].ToString();

            return cultureSlugs.Any(x => x.Key.ToString().Contains(culture, System.StringComparison.OrdinalIgnoreCase)
                   || x.Value.ToString().Contains(culture, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
