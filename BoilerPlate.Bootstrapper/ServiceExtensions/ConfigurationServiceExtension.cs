using BoilerPlate.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Bootstrapper
{
    public static class ConfigurationServiceExtension
    {
        public static IServiceCollection AddConfigurationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment
            , out ApplicationSettings applicationSettings
            , out ConnectionStrings connectionStrings)
        {
            connectionStrings = new ConnectionStrings();
            applicationSettings = new ApplicationSettings();

            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            configuration.GetSection("AppSettings").Bind(applicationSettings);
            applicationSettings.HostingEnvironmentPath = hostEnvironment.ContentRootPath;

            services.AddSingleton(connectionStrings);
            services.AddSingleton(applicationSettings);

            return services;
        }
    }
}
