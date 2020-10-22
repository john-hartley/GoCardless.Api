using Flurl.Http;
using GoCardlessApi.Exceptions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Exceptions
{
    public class FlurlHttpExceptionExtensionsTests
    {
        [Test]
        public async Task returns_api_exception_for_go_cardless_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(400, "gocardless");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ApiException>());
            Assert.That(result.Message, Does.Contain("Please contact GoCardless support"));
        }

        [Test]
        public async Task returns_invalid_api_usage_exception_for_invalid_api_usage_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(400, "invalid_api_usage");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<InvalidApiUsageException>());
        }

        [Test]
        public async Task returns_conflicting_resource_exception_for_conflict_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(409, "");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ConflictingResourceException>());
        }

        [Test]
        public async Task returns_invalid_state_exception_for_invalid_state_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(400, "invalid_state");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<InvalidStateException>());
        }

        [Test]
        public async Task returns_invalid_state_exception_for_validation_failed_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(400, "validation_failed");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ValidationFailedException>());
        }

        [Test]
        public async Task returns_api_exception_for_unknown_error()
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(400, "");
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ApiException>());
            Assert.That(result.Message, Does.Contain("Unknown API error type"));
        }

        [Test]
        public async Task returns_api_exception_when_unable_to_parse_api_error_response()
        {
            // given
            var httpCall = new HttpCall
            {
                FlurlRequest = new FlurlRequest(),
                Request = new HttpRequestMessage(),
                Response = new HttpResponseMessage()
                {
                    Content = new StringContent("invalid_json")
                }
            };
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ApiException>());
            Assert.That(result.Code, Is.EqualTo(500));
            Assert.That(result.Message, Does.Contain("An unexpected problem occurred while parsing the expected JSON response"));
        }

        private static ApiErrorResponse ApiErrorResponseFor(int code, string type)
        {
            return new ApiErrorResponse
            {
                Error = new ApiError
                {
                    Code = code,
                    Errors = new List<Error>(),
                    Type = type
                }
            };
        }

        private static HttpCall HttpCallFor(ApiErrorResponse apiErrorResponse)
        {
            var serialisedApiError = JsonConvert.SerializeObject(apiErrorResponse);

            return new HttpCall
            {
                FlurlRequest = new FlurlRequest(),
                Request = new HttpRequestMessage(),
                Response = new HttpResponseMessage()
                {
                    Content = new StringContent(serialisedApiError)
                }
            };
        }
    }
}