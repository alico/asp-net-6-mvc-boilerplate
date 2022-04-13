using System;

namespace BoilerPlate.Web.Entities
{
    public record OrderResponseModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
