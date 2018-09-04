using GoCardlessApi.BankDetailsLookups;
using GoCardlessApi.Core;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class BankDetailsLookupsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        public BankDetailsLookupsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [Test]
        public async Task LooksupBankDetails()
        {
            // given
            var subject = new BankDetailsLookupsClient(_configuration);

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