namespace BoilerPlate.Web.Entities
{
    public record UpdateOrderRequestModel
    {
        public UpdateOrderModel Order { get; set; }
        public UpdateUserModel User { get; set; }
    }
}
