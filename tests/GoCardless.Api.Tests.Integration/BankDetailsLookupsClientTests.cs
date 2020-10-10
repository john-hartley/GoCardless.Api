using GoCardless.Api.BankDetailsLookups;
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
            var subject = new BankDetailsLookupsClient(_apiClient);

            var options = new BankDetailsLookupOptions
            {
                AccountNumber = "55779911",
                BranchCode = "200000",
                CountryCode = "GB",
            };

            // when
            var result = await subject.LookupAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.AvailableDebitSchemes, Is.Not.Null.And.Not.Empty);
            Assert.That(result.Item.BankName, Is.Not.Null);
            Assert.That(result.Item.Bic, Is.Not.Null);
        }
    }
}