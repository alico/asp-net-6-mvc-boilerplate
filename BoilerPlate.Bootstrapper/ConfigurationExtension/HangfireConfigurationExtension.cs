using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace BoilerPlate.Bootstrapper
{
    public static class HangfireConfigurationExtension
    {
        public static IApplicationBuilder ConfigureHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                DashboardTitle = "BoilerPlate - Hangfire",
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            return app;
        }
    }
}
