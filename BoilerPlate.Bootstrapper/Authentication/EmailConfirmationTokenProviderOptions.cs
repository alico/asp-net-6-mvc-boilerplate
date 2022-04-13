using Microsoft.AspNetCore.Identity;

namespace BoilerPlate.Bootstrapper
{
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailDataProtectorTokenProvider";
            TokenLifespan = Utils.Constants.EmailConfirmationTokenExpiration;
        }
    }
}
