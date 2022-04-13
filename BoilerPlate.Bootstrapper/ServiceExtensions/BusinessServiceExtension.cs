using BoilerPlate.Service;
using BoilerPlate.Service.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BoilerPlate.Bootstrapper
{
    public static class BusinessServiceExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<IBulkOperationService, BulkOperationService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IEmailService, EmailService>();


            return services;
        }
    }
}
