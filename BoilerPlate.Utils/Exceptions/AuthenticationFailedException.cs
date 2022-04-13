namespace BoilerPlate.Utils
{
    public class AuthenticationFailedException : CustomException
    {
        public AuthenticationFailedException(string message = "Authentication failed!") : base(message)
        {

        }
    }
}