using BoilerPlate.Utils;
using System;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Contract
{
    public interface IEmailService
    {
        Task SendUserConfirmationEmail(Market market, ApplicationUser applicationUser, string confirmationLink);
    }
}
