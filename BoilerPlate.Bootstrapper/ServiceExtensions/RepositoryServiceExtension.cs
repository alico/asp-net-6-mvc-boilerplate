using BoilerPlate.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using BoilerPlate.Data.Contract;
using BoilerPlate.Data;
using System;

namespace BoilerPlate.Bootstrapper
{
    public static class RepositoryServiceExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            services.AddTransient<IDataContext, ApplicationDataContext>();
            services.AddTransient<IHangfireDataContext, HangfireDataContext>();

            //services.AddIdentityCore<ApplicationUser>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //    config.Tokens.ProviderMap.Add("CustomEmailConfirmation", new TokenProviderDescriptor(typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));
            //    config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            //}).AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();


            //        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<ApplicationRole>()
            //.AddEntityFrameworkStores<ApplicationDataContext>();

            //services.AddDefaultIdentity<ApplicationUser>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //    config.Tokens.ProviderMap.Add("CustomEmailConfirmation", new TokenProviderDescriptor(typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));
            //    config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            //}).AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();
            //////Identity Settings
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
                config.Tokens.ProviderMap.Add("CustomEmailConfirmation", new TokenProviderDescriptor(typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));
                config.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            }).AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDataContext>().AddDefaultTokenProviders();

            //Cookie Settings
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = Constants.CookieExpiration;
                options.SlidingExpiration = false;
                options.LoginPath = "/signup";
                options.LogoutPath = "/signout";
                options.AccessDeniedPath = "/error/403";
            });

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.AddTransient<CustomEmailConfirmationTokenProvider<ApplicationUser>>();

            services.AddDbContext<ApplicationDataContext>(options =>
             options.UseSqlServer(connectionStrings.MainConnection), ServiceLifetime.Transient);

            
            services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString =
                   connectionStrings.MainConnection;
                options.SchemaName = "dbo";
                options.TableName = "Cache";
            });

            var dbContext = services.BuildServiceProvider().GetService<IDataContext>();
            dbContext.EnsureDbCreated();

            return services;
        }
    }
}
