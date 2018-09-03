using GoCardlessApi.Core;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class RedirectFlowsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        public RedirectFlowsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [Test]
        public async Task CreatesRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var subject = new RedirectFlowsClient(_configuration);

            var request = new CreateRedirectFlowRequest
            {
                Description = "First redirect flow",
                Links = new CreateRedirectFlowLinks
                {
                    Creditor = creditor.Id
                },
                PrefilledCustomer = new PrefilledCustomer
                {
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    AddressLine3 = "Address Line 3",
                    City = "London",
                    CompanyName = "Company Name",
                    CountryCode = "GB",
                    DanishIdentityNumber = "2205506218",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "en",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                    SwedishIdentityNumber = "5302256218",
                },
                Scheme = "bacs",
                SessionToken = Guid.NewGuid().ToString(),
                SuccessRedirectUrl = "https://localhost",
            };

            // when
            var result = await subject.CreateAsync(request);
            var actual = result.RedirectFlow;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.ConfirmationUrl, Is.Null);
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Description, Is.EqualTo(request.Description));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.EqualTo(request.Links.Creditor));
            Assert.That(actual.Links.Customer, Is.Null);
            Assert.That(actual.Links.CustomerBankAccount, Is.Null);
            Assert.That(actual.RedirectUrl, Is.Not.Null);
            Assert.That(actual.Scheme, Is.EqualTo(request.Scheme));
            Assert.That(actual.SessionToken, Is.EqualTo(request.SessionToken));
            Assert.That(actual.SuccessRedirectUrl, Is.EqualTo(request.SuccessRedirectUrl));
        }
    }
}