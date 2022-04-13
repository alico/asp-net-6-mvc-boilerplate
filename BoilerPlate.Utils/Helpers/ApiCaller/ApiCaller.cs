using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BoilerPlate.Utils
{
    public static class ApiCaller<T> where T : class
    {
        public static async Task<APICallResponseDTO<T>> GetAsync(APICallRequestDTO request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var queryString = request.RequestObject.ToQueryString();
                    var url = RequestHelpers.GetRequestUrl(request.Url, request.MethodName, queryString);

                    client.DefaultRequestHeaders.Accept.Clear();

                    if (!string.IsNullOrEmpty(request.Token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.ApplicationJson.GetDescription()));

                    if (request.RequestHeader != null)
                        foreach (var parameter in request.RequestHeader)
                        {
                            client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                        }

                    using (HttpResponseMessage httpResponse = (await client.GetAsync(url)))
                    {
                        using HttpContent content = httpResponse.Content;
                        var json = await content.ReadAsStringAsync();

                        var apiResponse = new APICallResponseDTO<T>();
                        apiResponse.StatusCode = (int)httpResponse.StatusCode;

                        if (httpResponse.IsSuccessStatusCode)
                            apiResponse.Body = JsonConvert.DeserializeObject<T>(json);
                        //Errors are returned in many different formats.  
                        //We just need to log the message so don't need to bind it to an object. 
                        else
                            apiResponse.Error = json;

                        return apiResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex);
            }
        }

        public static async Task<APICallResponseDTO<T>> PostAsync(APICallRequestDTO request)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var url = RequestHelpers.GetRequestUrl(request.Url, request.MethodName);
                var postContent = RequestHelpers.CreateRequestBodyContent(request.RequestObject, request.ContentType);

                client.DefaultRequestHeaders.Accept.Clear();

                if (!string.IsNullOrEmpty(request.Token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.ApplicationJson.GetDescription()));

                if (request.RequestHeader != null)
                    foreach (var parameter in request.RequestHeader)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(parameter.Key, parameter.Value);
                    }

                var requestJson = JsonConvert.SerializeObject(request.RequestObject);

                using (HttpResponseMessage httpResponse = (await client.PostAsync(url, postContent)))
                {
                    using HttpContent content = httpResponse.Content;
                    var json = await content.ReadAsStringAsync();

                    var apiResponse = new APICallResponseDTO<T>();
                    apiResponse.StatusCode = (int)httpResponse.StatusCode;

                    if (httpResponse.IsSuccessStatusCode)
                        apiResponse.Body = JsonConvert.DeserializeObject<T>(json);
                    else
                        apiResponse.Error = json;

                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex);
            }
        }

        public static async Task<APICallResponseDTO<T>> PutAsync(APICallRequestDTO request)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var url = RequestHelpers.GetRequestUrl(request.Url, request.MethodName);
                var postContent = RequestHelpers.CreateRequestBodyContent(request.RequestObject, request.ContentType);
                var contentJson = JsonConvert.SerializeObject(request);
                client.DefaultRequestHeaders.Accept.Clear();

                if (!string.IsNullOrEmpty(request.Token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.ApplicationJson.GetDescription()));

                if (request.RequestHeader != null)
                    foreach (var parameter in request.RequestHeader)
                    {
                        client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                    }

                using (HttpResponseMessage httpResponse = (await client.PutAsync(url, postContent)))
                {
                    using HttpContent content = httpResponse.Content;
                    var json = await content.ReadAsStringAsync();

                    var apiResponse = new APICallResponseDTO<T>();
                    apiResponse.StatusCode = (int)httpResponse.StatusCode;

                    if (httpResponse.IsSuccessStatusCode)
                        apiResponse.Body = JsonConvert.DeserializeObject<T>(json);
                    else
                        apiResponse.Error = json;

                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex);
            }
        }

        public static async Task<APICallResponseDTO<T>> DeleteAsync(APICallRequestDTO request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var queryString = request.RequestObject.ToQueryString();
                    var url = RequestHelpers.GetRequestUrl(request.Url, request.MethodName, queryString);

                    client.DefaultRequestHeaders.Accept.Clear();

                    if (!string.IsNullOrEmpty(request.Token))
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.ApplicationJson.GetDescription()));

                    if (request.RequestHeader != null)
                        foreach (var parameter in request.RequestHeader)
                        {
                            client.DefaultRequestHeaders.Add(parameter.Key, parameter.Value);
                        }

                    using (HttpResponseMessage httpResponse = (await client.DeleteAsync(url)))
                    {
                        using HttpContent content = httpResponse.Content;
                        var json = await content.ReadAsStringAsync();

                        var apiResponse = new APICallResponseDTO<T>();
                        apiResponse.StatusCode = (int)httpResponse.StatusCode;

                        if (httpResponse.IsSuccessStatusCode)
                            apiResponse.Body = JsonConvert.DeserializeObject<T>(json);
                        //Errors are returned in many different formats.  
                        //We just need to log the message so don't need to bind it to an object. 
                        else
                            apiResponse.Error = json;

                        return apiResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex);
            }
        }
    }
}
