using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record OrderModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Order Id is required.")]
        public string Id { get; set; }
    }
}
