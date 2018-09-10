using GoCardless.Api.BankDetailsLookups;
using GoCardless.Api.Core;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class BankDetailsLookupsClientTests : IntegrationTest
    {
        [Test]
        public async Task LooksupBankDetails()
        {
            // given
            var subject = new BankDetailsLookupsClient(_clientConfiguration);

            var request = new BankDetailsLookupRequest
            {
                AccountNumber = "55779911",
                BranchCode = "200000",
                CountryCode = "GB",
            };

            // when
            var result = await subject.LookupAsync(request);

            // then
            Assert.That(result.BankDetailsLookup, Is.Not.Null);
            Assert.That(result.BankDetailsLookup.AvailableDebitSchemes, Is.Not.Null.And.Not.Empty);
            Assert.That(result.BankDetailsLookup.BankName, Is.Not.Null);
            Assert.That(result.BankDetailsLookup.Bic, Is.Not.Null);
        }
    }
}