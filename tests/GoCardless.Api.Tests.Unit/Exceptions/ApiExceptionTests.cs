using GoCardlessApi.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace GoCardlessApi.Tests.Unit.Exceptions
{
    public class ApiExceptionTests
    {
        [Test]
        public void returns_sensible_default_when_no_message_and_no_errors_provided()
        {
            // given
            var apiError = new ApiError
            {
                Message = null
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.Message, Is.EqualTo("An unknown API error occurred. Please see the RawResponse property for more information."));
        }

        [Test]
        public void returns_sensible_default_when_no_message_provided_and_no_errors_have_a_message()
        {
            // given
            var apiError = new ApiError
            {
                Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = null
                    }
                },
                Message = null
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.Message, Is.EqualTo("An unknown API error occurred. Please see the RawResponse property for more information."));
        }

        [Test]
        public void returns_message_when_no_errors_have_a_message()
        {
            // given
            var apiError = new ApiError
            {
                Errors = new List<Error>()
                {
                    new Error
                    {
                        Message = null
                    }
                },
                Message = "message"
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.Message, Is.EqualTo("message"));
        }

        [Test]
        public void ignores_field_when_field_not_provided()
        {
            // given
            var apiError = new ApiError
            {
                Errors = new List<Error>()
                {
                    new Error
                    {
                        Field = null,
                        Message = "message"
                    }
                }
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.Message, Is.EqualTo("message"));
        }

        [Test]
        public void prefixes_message_with_field_when_field_provided()
        {
            // given
            var apiError = new ApiError
            {
                Errors = new List<Error>()
                {
                    new Error
                    {
                        Field = "field1",
                        Message = "message"
                    }
                }
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.Message, Is.EqualTo("field1: message"));
        }

        [Test]
        public void returns_no_resource_id_when_no_conflict_found()
        {
            // given
            var apiError = new ApiError();

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.ResourceId, Is.Null);
        }

        [TestCase("bank_account_exists", "creditor_bank_account", "CBA123456")]
        [TestCase("bank_account_exists", "customer_bank_account", "CBA123456")]
        [TestCase("idempotent_creation_conflict", "conflicting_resource_id", "CRID123456")]
        public void returns_conflicting_resource_id(string reason, string link, string expected)
        {
            // given
            var apiError = new ApiError
            {
                Errors = new List<Error>()
                {
                    new Error
                    {
                        Reason = reason,
                        Links = new Dictionary<string, string>
                        {
                            [link] = expected
                        }
                    }
                }
            };

            // when
            var result = new ApiException(apiError);

            // then
            Assert.That(result.ResourceId, Is.EqualTo(expected));
        }
    }
}