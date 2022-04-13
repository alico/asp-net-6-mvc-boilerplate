namespace BoilerPlate.Utils
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message = "Couldn't find this item!") : base(message)
        {

        }
    }
}