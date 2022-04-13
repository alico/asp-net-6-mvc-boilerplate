using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Web.Entities
{
    public record UpdateOrderModel
    {
        [StringLength(50)]
        public string Id { get; set; }
    }
}
