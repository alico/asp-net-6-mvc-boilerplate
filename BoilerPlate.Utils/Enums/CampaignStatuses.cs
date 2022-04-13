using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    [Serializable]
    public enum CampaignStatuses
    {
        None = 0,
        Open = 1,
        ClosePreSeason = 2,
        ClosedForSeason = 3
    }
}
