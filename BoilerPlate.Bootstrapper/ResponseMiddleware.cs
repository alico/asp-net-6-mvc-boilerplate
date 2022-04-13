using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BoilerPlate.Bootstrapper
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IIdentityService identityService, ILogger<ResponseMiddleware> logger, IWebHostEnvironment env, ApplicationSettings applicationSettings)
        {
            string requestId = GetRequestId(context);
            context.Items["CorrelationId"] = requestId;

            if (context.Request.Path.Value.Contains("/api/orders", StringComparison.InvariantCultureIgnoreCase)
                || context.Request.QueryString.Value.Contains(HttpUtility.UrlEncode("/api/orders"), StringComparison.InvariantCultureIgnoreCase))
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                var user = await identityService.AuthenticateAPIUser(token);
                if ((token == null || user == null) && context.Request.Path != "/api/orders/associate")
                {
                    var authenticationException = new AuthenticationFailedException("Authentication error!");
                    logger.LogError(authenticationException, "{Message}{RequestId}", $"{context.Connection.RemoteIpAddress} - {authenticationException.Message}", requestId);
                    context.Response.Clear();
                    await HandleExceptionAsync(context, authenticationException, requestId);
                }
                else
                {
                    context.Items["User"] = user;
                    await Next(context, logger, env, requestId);
                }

            }
        
            else
                await Next(context, logger, env, requestId);
        }

        private async Task Next(HttpContext context, ILogger<ResponseMiddleware> logger, IWebHostEnvironment env, string requestId)
        {
            if (env.IsDevelopment())
                await _next(context);
            else
            {
                try
                {
                    await _next(context);

                    if (!(context.Request.Path.Value.Contains("/api") || context.Request.IsAjaxRequest()) &&
                        context.Response.StatusCode == 404)
                        context.Response.Redirect("/page-not-found");
                }
                catch (CustomException ex)
                {
                    logger.LogError(ex, "{Message}{RequestId}", ex.Message, requestId);

                    if (context.Request.Path.Value.Contains("/api") || context.Request.IsAjaxRequest())
                        await HandleExceptionAsync(context, ex, requestId);
                    else
                        context.Response.Redirect($"/error/{requestId}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{Message}{RequestId}", ex.Message, requestId);

                    if (context.Request.Path.Value.Contains("/api") || context.Request.IsAjaxRequest())
                        await HandleExceptionAsync(context, ex, requestId);
                    else
                        context.Response.Redirect($"/error/{requestId}");
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
        {
            context.Response.Clear();
            var apiResponse = new APIErrorResponseWrapper();
            var apiError = ApiErrorResponseFactory.Create(exception);
            apiError.CorrelationId = correlationId;
            context.Response.StatusCode = apiError.StatusCode;
            context.Response.ContentType = "application/json";
            apiResponse.Error = apiError;

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static string GetRequestId(HttpContext context)
        {
            string requestId;
            var header = context.Request.Headers["CorrelationId"];
            if (header.Count > 0)
                requestId = header[0];
            else
                requestId = Guid.NewGuid().ToString();

            return requestId;
        }
    }
}
