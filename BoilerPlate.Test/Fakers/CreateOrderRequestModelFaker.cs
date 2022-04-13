using Bogus;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Test
{
    public class CreateOrderRequestModelFaker : Faker<CreateOrderRequestModel>
    {
        public CreateOrderRequestModelFaker()
        {
            RuleFor(x => x.Order, new OrderModelFaker().Generate());
        }
    }
}