using System.Collections.Generic;

namespace BoilerPlate.Utils
{
    public class APICallRequestDTO
    {
        public string Url { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, string> RequestHeader { get; set; }
        public object RequestObject { get; set; }
        public ContentType ContentType { get; set; }
        public string Token { get; set; }

        public APICallRequestDTO()
        {
            ContentType = ContentType.ApplicationJson;
            Token = string.Empty;
            RequestHeader = new Dictionary<string, string>();
        }
    }
}
