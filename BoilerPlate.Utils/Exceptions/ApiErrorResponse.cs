using Newtonsoft.Json;
using System.Collections.Generic;
namespace BoilerPlate.Utils
{
    public class ApiErrorResponse
    {
        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }


        [JsonProperty("message")]
        public List<string> Message { get; private set; }

        public ApiErrorResponse(int statusCode, string description)
        {
            this.StatusCode = statusCode;
            Message = new List<string>();
            this.Message.Add(description);
        }
        public ApiErrorResponse(int statusCode, List<string> description)
        {
            this.StatusCode = statusCode;
            this.Message = description;
        }
    }
}
