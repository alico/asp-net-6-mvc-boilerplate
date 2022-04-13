using Microsoft.AspNetCore.Identity;
using System;

namespace BoilerPlate.Utils
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string name):base(name)
        {

        }
    }
}