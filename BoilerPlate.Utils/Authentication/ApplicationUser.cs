using System;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerPlate.Utils
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }

        [NotMapped]
        public Countries Country { get; set; }

        public bool IsOptInMarketing { get; set; }
        public bool IsApprovedTermsConditions { get; set; }
        public bool IsOverThanAgeLimit { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastmodifyDate { get; set; }
    }
}
