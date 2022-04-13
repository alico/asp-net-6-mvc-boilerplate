using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    public static class Constants
    {
        public static readonly short CampaignSettings = 1;
        public static readonly string SeasonStartDate = "SeasonStartDate";
        public static readonly string SeasonEndDateKey = "SeasonEndDate";
        public static readonly string CampaignCacheKey = "Campaign";
        public static readonly string ISODateFormat = "yyyy-MM-dd hh:mm";
        public static readonly string ClientSecret = "ClientSecret";
        public static readonly string AdminUserPassword = "qwert123";
        public static readonly string APIUserPassword = "qwert123";

        public const string Admin = "Admin";

        public static readonly string SymetricKey = "287f2d39-2c26-41e3-9896-ca6fb220d426";
        public static readonly string DataProtectorToken = "3a1298f3-574e-4b48-b274-986d9cc918b8";
        public static readonly string EncryptionSymetrickey = "GTY@!$QPUTXMZLH";
        public static readonly byte[] EncryptionIv = new byte[] { 0x30, 0x26, 0x45, 0x5e, 0x10, 0x6d, 0x35, 0x62, 0x26, 0x65, 0x64, 0x15, 0x36 };
        public static readonly string Statistics = "Statistics";

        public static readonly TimeSpan EmailConfirmationTokenExpiration = TimeSpan.FromDays(180);
        public static readonly TimeSpan CookieExpiration = TimeSpan.FromDays(180);
        public static readonly TimeSpan UserSessionExpiration = TimeSpan.FromDays(180);
        public static short HangfireWorkerCount = 1;
    }
}
