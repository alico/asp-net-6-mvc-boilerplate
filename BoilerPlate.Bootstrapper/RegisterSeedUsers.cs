using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace BoilerPlate.Bootstrapper
{
    public class RegisterSeedUsers
    {
        public static void Initialize()
        {
            var jobId = BackgroundJob.Enqueue<IIdentityService>(x => x.CreateRoles());
            BackgroundJob.ContinueJobWith<IIdentityService>(jobId, x => x.CreateAdminUsers());
            BackgroundJob.ContinueJobWith<IIdentityService>(jobId, x => x.CreateAPIUsers());
        }
    }
}
