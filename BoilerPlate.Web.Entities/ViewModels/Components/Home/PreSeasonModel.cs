
namespace BoilerPlate.Web.Entities
{
    public record PreSeasonModel : CountryBaseModel
    {
        public SignUpModel SignUpModel { get; set; }
        public string OpeningDate { get; set; }
    }
}