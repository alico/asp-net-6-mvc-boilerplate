using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record CreateOrderRequestModel
    {
        [Required(ErrorMessage = "Order object must be set.")]
        public OrderModel Order { get; set; }


        [Required(ErrorMessage = "User object must be set.")]
        public UserModel User { get; set; }
    }

}
