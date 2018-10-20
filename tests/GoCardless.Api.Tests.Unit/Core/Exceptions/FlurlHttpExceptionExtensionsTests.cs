﻿using Flurl.Http;
using GoCardless.Api.Core.Exceptions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit.Core.Exceptions
{
    public class FlurlHttpExceptionExtensionsTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("Test error type")]
        public async Task HttpConflictReturnsResourceAlreadyExistsException(string type)
        {
            // given
            var apiErrorResponse = ApiErrorResponseFor(409, type);
            var httpCall = HttpCallFor(apiErrorResponse);
            var exception = new FlurlHttpException(httpCall);

            // when
            var result = await exception.CreateApiExceptionAsync();

            // then
            Assert.That(result, Is.InstanceOf<ResourceAlreadyExistsException>());
        }

        [Test]
        public async Task GoCardlessErrorTypeReturnsApiException()
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
        public async Task InvalidApiUsageErrorTypeReturnsInvalidApiUsageException()
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
        public async Task InvalidStateErrorTypeReturnsInvalidStateException()
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
        public async Task ValidationFailedErrorTypeReturnsInvalidStateException()
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
        public async Task UnknownErrorTypeReturnsApiException()
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
        public async Task CannotParseApiErrorResponseReturnsApiException()
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