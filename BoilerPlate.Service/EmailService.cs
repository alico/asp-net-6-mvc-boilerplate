using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Hangfire;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Service
{
    public class EmailService : IEmailService
    {
        public readonly IMemoryCache _cache;
        public readonly IDistributedCache _distributedCache;

        public EmailService(IMemoryCache cache, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _cache = cache;
        }

        public async Task SendUserConfirmationEmail(Market market, ApplicationUser applicationUser, string confirmationLink)
        {
            //BackgroundJob.Enqueue();
        }
    }
}
