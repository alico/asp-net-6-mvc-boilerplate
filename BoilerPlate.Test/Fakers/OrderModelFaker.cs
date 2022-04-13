using Bogus;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Test
{
    public class OrderModelFaker : Faker<OrderModel>
    {
        public OrderModelFaker()
        {
            RuleFor(x => x.Id, x => x.Random.AlphaNumeric(10));
            //RuleFor(x => x.PostCode, x => x.Address.ZipCode("##? ?##"));
            //RuleFor(x => x.SKU, x => x.Commerce.Ean8());
        }
    }
}