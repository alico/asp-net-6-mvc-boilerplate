using System;

namespace BoilerPlate.Utils
{
    public class ApiErrorResponseFactory
    {
        public static ApiErrorResponse Create(Exception ex)
        {
            if (ex.GetType() == typeof(NotFoundException))
                return new ApiErrorResponse(404, ex.Message);

            else if (ex.GetType() == typeof(ForbiddenException))
                return new ApiErrorResponse(403, ex.Message);

            else if (ex.GetType() == typeof(AuthenticationFailedException))
                return new ApiErrorResponse(401, ex.Message);

            else if (ex.GetType() == typeof(CustomException))
            {
                if (((CustomException)ex).Messages != null)
                    return new ApiErrorResponse(400, ((CustomException)ex).Messages);
                else
                    return new ApiErrorResponse(400, ex.Message);
            }
            else
                return new ApiErrorResponse(400, "An error occurred during processing the request.");
        }
    }
}
