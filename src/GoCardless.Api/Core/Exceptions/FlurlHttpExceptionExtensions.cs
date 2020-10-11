using Flurl.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoCardless.Api.Core.Exceptions
{
    public static class FlurlHttpExceptionExtensions
    {
        public static async Task<ApiException> CreateApiExceptionAsync(this FlurlHttpException source)
        {
            var rawResponse = await source.GetResponseStringAsync();

            try
            {
                var apiErrorResponse = await source
                    .GetResponseJsonAsync<ApiErrorResponse>()
                    .ConfigureAwait(false);

                apiErrorResponse.Error.RawResponse = rawResponse;

                return CreateApiExceptionFrom(source, apiErrorResponse);
            }
            catch (JsonException ex)
            {
                var message = "An unexpected problem occurred while parsing the expected JSON response. See the RawResponse property, and the inner exception, for details.";

                return new ApiException(
                    message,
                    source,
                    new ApiError
                    {
                        Code = 500,
                        Message = ex.Message,
                        RawResponse = rawResponse,
                        Type = "gocardless"
                    });
            }
        }

        private static ApiException CreateApiExceptionFrom(
            FlurlHttpException flurlHttpException,
            ApiErrorResponse apiErrorResponse)
        {
            var error = apiErrorResponse.Error;
            switch (error.Type)
            {
                case "gocardless":
                    return new ApiException($"An internal error occurred. Please contact GoCardless support, quoting '{error.RequestId}' as the id of the request.", error);
                case "invalid_api_usage":
                    return new InvalidApiUsageException(error.Message, error);
                case "invalid_state":
                    if (error.Code == 409)
                    {
                        return new ConflictingResourceException(error.Message, error);
                    }
                    return new InvalidStateException(error.Message, error);
                case "validation_failed":
                    return new ValidationFailedException(error.Message, error);
                default:
                    return new ApiException($"Unknown API error type '{error.Type}' found. See the RawResponse property, and the inner exception, for details.", flurlHttpException, error);
            }
        }
    }
}