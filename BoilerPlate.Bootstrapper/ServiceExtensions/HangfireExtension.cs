using BoilerPlate.Data.Contract;
using BoilerPlate.Utils;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BoilerPlate.Bootstrapper
{
    public static class HangfireExtension
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            var hangfireDbContext = services.BuildServiceProvider().GetService<IHangfireDataContext>();
            hangfireDbContext.EnsureDbCreated();

            services.AddHangfire(configuration => configuration
          .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(connectionStrings.HangfireConnection, new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.FromSeconds(15),
              UseRecommendedIsolationLevel = true,
              UsePageLocksOnDequeue = true,
              DisableGlobalLocks = true,
          }));

            services.AddHangfireServer(options =>
            {
                options.ServerName = String.Format("{0}.{1}", Environment.MachineName, Guid.NewGuid().ToString());
                options.WorkerCount = Constants.HangfireWorkerCount;
                options.Queues = new[] {
                      HangfireQueues.Default,
                    };
            });

            services.AddHangfireServer(options =>
            {
                options.ServerName = String.Format("{0}.{1}", Environment.MachineName, HangfireQueues.ReminderEmails.ToString());
                options.WorkerCount = 1;
                options.Queues = new[] {
                      HangfireQueues.ReminderEmails,
                    };
            });

            return services;
        }
    }
}
