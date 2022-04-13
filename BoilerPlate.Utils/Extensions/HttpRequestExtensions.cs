using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;

namespace BoilerPlate.Utils
{
    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";
        const string jsonMime = "application/json";

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            bool rtn = false;
            if (request.Headers != null)
            {
                bool isFetch = false;
                bool isAjax = request.Headers[RequestedWithHeader] == XmlHttpRequest;
                string acceptTypes = request.Headers[HeaderNames.Accept];

                if (!string.IsNullOrEmpty(acceptTypes))
                {
                    rtn = acceptTypes.ToLower().Contains(jsonMime, StringComparison.OrdinalIgnoreCase);
                    if (request.ContentType != null)
                        isFetch = rtn || request.ContentType.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Any(t => t.Equals(jsonMime, StringComparison.OrdinalIgnoreCase));
                }

                return isAjax || isFetch;
            }



            return false;
        }
    }
}
