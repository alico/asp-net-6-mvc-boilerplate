using Newtonsoft.Json;

namespace BoilerPlate.Utils
{
    public class APIErrorResponseWrapper
    {
        [JsonProperty("error")]
        public object Error { get; set; }
    }
}
