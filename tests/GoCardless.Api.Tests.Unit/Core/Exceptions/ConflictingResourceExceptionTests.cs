using GoCardless.Api.Core.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace GoCardless.Api.Tests.Unit.Core.Exceptions
{
    public class ConflictingResourceExceptionTests
    {
        [Test]
        public void ResourceIdReturnsNullWhenNoKeyFound()
        {
            // given
            var apiError = new ApiError();

            // when
            var result = new ConflictingResourceException("", apiError);

            // then
            Assert.That(result.ResourceId, Is.Null);
        }

        [TestCase("bank_account_exists", "creditor_bank_account", "CBA123456")]
        [TestCase("bank_account_exists", "customer_bank_account", "CBA123456")]
        [TestCase("idempotent_creation_conflict", "conflicting_resource_id", "CRID123456")]
        public void ReturnsConflictingResourceId(string reason, string link, string expected)
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
            var result = new ConflictingResourceException("", apiError);

            // then
            Assert.That(result.ResourceId, Is.EqualTo(expected));
        }
    }
}