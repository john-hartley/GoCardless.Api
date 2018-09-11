using GoCardless.Api.MandatePdfs;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class MandatePdfsClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesMandatePdf()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            var mandate = await _resourceFactory.CreateMandateFor(creditor, customer, customerBankAccount);
            var subject = new MandatePdfsClient(_clientConfiguration);

            var request = new CreateMandatePdfRequest
            {
                Language = "en",
                Links = new MandatePdfLinks
                {
                    Mandate = mandate.Id
                }
            };

            // when
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.MandatePdf, Is.Not.Null);
            Assert.That(result.MandatePdf.ExpiresAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.MandatePdf.Url, Is.Not.Null);
        }
    }
}