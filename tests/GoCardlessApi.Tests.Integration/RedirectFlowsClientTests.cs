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
        public async Task CreatesAndReturnsRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var subject = new RedirectFlowsClient(_configuration);

            var createRequest = new CreateRedirectFlowRequest
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
            var createResult = await subject.CreateAsync(createRequest);
            var result = await subject.ForIdAsync(createResult.RedirectFlow.Id);

            // then
            Assert.That(createResult.RedirectFlow.ConfirmationUrl, Is.Null);
            Assert.That(createResult.RedirectFlow.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.RedirectFlow.Description, Is.EqualTo(createRequest.Description));
            Assert.That(createResult.RedirectFlow.Links, Is.Not.Null);
            Assert.That(createResult.RedirectFlow.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(createResult.RedirectFlow.Links.Customer, Is.Null);
            Assert.That(createResult.RedirectFlow.Links.CustomerBankAccount, Is.Null);
            Assert.That(createResult.RedirectFlow.RedirectUrl, Is.Not.Null);
            Assert.That(createResult.RedirectFlow.Scheme, Is.EqualTo(createRequest.Scheme));
            Assert.That(createResult.RedirectFlow.SessionToken, Is.EqualTo(createRequest.SessionToken));
            Assert.That(createResult.RedirectFlow.SuccessRedirectUrl, Is.EqualTo(createRequest.SuccessRedirectUrl));

            Assert.That(createResult.RedirectFlow, Is.Not.Null);
            Assert.That(createResult.RedirectFlow.Id, Is.Not.Null.And.EqualTo(createResult.RedirectFlow.Id));
            Assert.That(createResult.RedirectFlow.ConfirmationUrl, Is.Null);
            Assert.That(createResult.RedirectFlow.CreatedAt, Is.Not.Null.And.Not.EqualTo(createResult.RedirectFlow.Id));
            Assert.That(createResult.RedirectFlow.Description, Is.EqualTo(createResult.RedirectFlow.Description));
            Assert.That(createResult.RedirectFlow.Links, Is.Not.Null);
            Assert.That(createResult.RedirectFlow.Links.Creditor, Is.EqualTo(createResult.RedirectFlow.Links.Creditor));
            Assert.That(createResult.RedirectFlow.Links.Customer, Is.Null);
            Assert.That(createResult.RedirectFlow.Links.CustomerBankAccount, Is.Null);
            Assert.That(createResult.RedirectFlow.RedirectUrl, Is.Not.Null.And.EqualTo(createResult.RedirectFlow.RedirectUrl));
            Assert.That(createResult.RedirectFlow.Scheme, Is.EqualTo(createResult.RedirectFlow.Scheme));
            Assert.That(createResult.RedirectFlow.SessionToken, Is.EqualTo(createResult.RedirectFlow.SessionToken));
            Assert.That(createResult.RedirectFlow.SuccessRedirectUrl, Is.EqualTo(createResult.RedirectFlow.SuccessRedirectUrl));
        }
    }
}