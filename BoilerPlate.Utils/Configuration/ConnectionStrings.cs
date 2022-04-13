using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    public class ConnectionStrings
    {
        public string MainConnection { get; set; }
        public string HangfireConnection { get; set; }
    }
}
