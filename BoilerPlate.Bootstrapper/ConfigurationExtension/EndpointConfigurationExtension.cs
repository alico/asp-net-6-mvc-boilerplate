using BoilerPlate.Utils;
using Microsoft.AspNetCore.Builder;

namespace BoilerPlate.Bootstrapper
{
    public static class EndpointConfigurationExtension
    {
        public static IApplicationBuilder ConfigureEndpoints(this IApplicationBuilder app, ApplicationSettings applicationSettings)
        {
            if (applicationSettings.IsDevelopment() || applicationSettings.IsTest())
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture:culture}/{controller=Home}/{action=Index}/{id?}", new
                    {
                        culture = "en"
                    });
                });
            }
            else
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }
            return app;
        }
    }
}
