using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record UpdateUserModel
    {
        [StringLength(50)]
        public string Token { get; set; }
    }
}
