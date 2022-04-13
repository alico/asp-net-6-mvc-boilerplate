namespace BoilerPlate.Utils
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message = "Not Authorized.") : base(message)
        {

        }
    }
}