using GoCardless.Api.Core.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace GoCardless.Api.Tests.Unit.Core.Exceptions
{
    public class ResourceAlreadyExistsExceptionTests
    {
        [Test]
        public void ReturnsExistingCreditorBankAccountId()
        {
            // given
            var expected = "BA12345678";

            var apiError = new ApiError
            {
                Errors = new List<Error>
                {
                    new Error
                    {
                        Links = new Dictionary<string, string>
                        {
                            { "creditor_bank_account", expected }
                        },
                        Reason = "bank_account_exists"
                    }
                }
            };

            var subject = new ResourceAlreadyExistsException("", apiError);

            // when
            var result = subject.ResourceId;

            // then
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReturnsExistingCustomerBankAccountId()
        {
            // given
            var expected = "BA12345678";

            var apiError = new ApiError
            {
                Errors = new List<Error>
                {
                    new Error
                    {
                        Links = new Dictionary<string, string>
                        {
                            { "customer_bank_account", expected }
                        },
                        Reason = "bank_account_exists"
                    }
                }
            };

            var subject = new ResourceAlreadyExistsException("", apiError);

            // when
            var result = subject.ResourceId;

            // then
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReturnsConflictingResourceId()
        {
            // given
            var expected = "RX12345678";

            var apiError = new ApiError
            {
                Errors = new List<Error>
                {
                    new Error
                    {
                        Links = new Dictionary<string, string>
                        {
                            { "conflicting_resource_id", expected }
                        },
                        Reason = "idempotent_creation_conflict"
                    }
                }
            };

            var subject = new ResourceAlreadyExistsException("", apiError);

            // when
            var result = subject.ResourceId;

            // then
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}