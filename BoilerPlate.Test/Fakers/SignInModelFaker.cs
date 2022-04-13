using Bogus;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Test
{
    public class SignInModelFaker : Faker<SignInModel>
    {
        public SignInModelFaker()
        {
            RuleFor(x => x.UserName, x => x.Random.AlphaNumeric(10));
            RuleFor(x => x.Password, x => x.Random.AlphaNumeric(10));
        }
    }
}