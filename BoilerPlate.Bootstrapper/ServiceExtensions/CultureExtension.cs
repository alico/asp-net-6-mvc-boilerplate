using BoilerPlate.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace BoilerPlate.Bootstrapper
{
    public static class CultureExtension
    {
        public static IServiceCollection AddCulture(this IServiceCollection services, ApplicationSettings applicationSettings, MarketConfiguration marketConfiguration)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            //dev or test env INSERT culture constraint into the route'
            if (applicationSettings.IsDevelopment() || applicationSettings.IsTest())
            {
                services.Configure<RouteOptions>(options =>
                {
                    options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
                });
                services.AddMvc(opts =>
                {
                    opts.Conventions.Insert(0, new LocalizationConvention());
                }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                         .AddDataAnnotationsLocalization()
                         .AddJsonOptions(options =>
                         {
                             options.JsonSerializerOptions.IgnoreNullValues = true;
                             options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                         });
            }
            else
            {
                services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                         .AddDataAnnotationsLocalization()
                         .AddJsonOptions(options =>
                         {
                             options.JsonSerializerOptions.IgnoreNullValues = true;
                             options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                         });
            }

            var supportedCultures = marketConfiguration.SupportedCultures.Values.Select(x => new CultureInfo(x)).ToList();
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    options.DefaultRequestCulture = new RequestCulture(marketConfiguration.DefaultCulture);
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders = new[]{ new RouteDataRequestCultureProvider{
                        IndexOfCulture=1,
                        ApplicationSettings = applicationSettings,
                        MarketConfiguration = marketConfiguration
                    }};
                });

            return services;
        }
    }
}
