using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record UserModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "User token is required.")]
        public string Token { get; set; }
    }
}
