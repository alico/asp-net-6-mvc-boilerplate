using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record SignInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
