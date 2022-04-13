using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    public class HangfireQueues
    {
        public const string Default = "default";
        public const string CampaignStatus = "campaignstatus";
        public const string ReminderEmails = "reminderemails";
    }
}
