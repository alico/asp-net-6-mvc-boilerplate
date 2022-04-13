using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BoilerPlate.Utils
{
    [Serializable]
    public enum Countries
    {
        None = 0,
        [Description("en-GB")]
        UK = 1,
        [Description("en-Au")]
        Au = 2,
    }
}
