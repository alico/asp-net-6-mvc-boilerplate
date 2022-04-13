using BoilerPlate.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record SignUpModel : CountryBaseModel
    {

        [StringLength(20)]
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }

        [StringLength(10)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        [IsTrue]
        [Required(ErrorMessage = "Terms&Conditions must be checked.")]
        public bool TermsConditions { get; set; }

        [IsTrue]
        [Required(ErrorMessage = "You must be 16 years or older to use this service.")]
        public bool AgeCheck { get; set; }

        public bool MarketingOptIn { get; set; }

        public string TermsConditionsHTML { get; set; }

        public string ReturnUrl { get; set; }

    }
}
