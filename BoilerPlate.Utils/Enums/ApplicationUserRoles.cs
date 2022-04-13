using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    public enum ApplicationUserRoles
    {
        None = 0,
        ApplicationUser = 1,
        Admin = 2,
        APIUser = 3
    }
}
