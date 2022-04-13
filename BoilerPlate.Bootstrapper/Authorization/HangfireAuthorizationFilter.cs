using BoilerPlate.Utils;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Globalization;

namespace BoilerPlate.Bootstrapper
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.IsInRole(ApplicationUserRoles.Admin.ToString());
        }
    }
}
