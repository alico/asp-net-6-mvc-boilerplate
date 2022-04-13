using BoilerPlate.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record SettingsModel
    {
        [Required(ErrorMessage = "StartDate must be set. ")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate must be set. ")]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string APISecret { get; set; }

        public Countries Country { get; set; }
    }
}
