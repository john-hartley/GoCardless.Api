using GoCardless.Api.Core;
using GoCardless.Api.RedirectFlows;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
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

            Assert.That(result.RedirectFlow, Is.Not.Null);
            Assert.That(result.RedirectFlow.Id, Is.Not.Null.And.EqualTo(createResult.RedirectFlow.Id));
            Assert.That(result.RedirectFlow.ConfirmationUrl, Is.Null);
            Assert.That(result.RedirectFlow.CreatedAt, Is.Not.Null.And.Not.EqualTo(createResult.RedirectFlow.Id));
            Assert.That(result.RedirectFlow.Description, Is.EqualTo(createResult.RedirectFlow.Description));
            Assert.That(result.RedirectFlow.Links, Is.Not.Null);
            Assert.That(result.RedirectFlow.Links.Creditor, Is.EqualTo(createResult.RedirectFlow.Links.Creditor));
            Assert.That(result.RedirectFlow.Links.Customer, Is.Null);
            Assert.That(result.RedirectFlow.Links.CustomerBankAccount, Is.Null);
            Assert.That(result.RedirectFlow.Links.Mandate, Is.Null);
            Assert.That(result.RedirectFlow.RedirectUrl, Is.Not.Null.And.EqualTo(createResult.RedirectFlow.RedirectUrl));
            Assert.That(result.RedirectFlow.Scheme, Is.EqualTo(createResult.RedirectFlow.Scheme));
            Assert.That(result.RedirectFlow.SessionToken, Is.EqualTo(createResult.RedirectFlow.SessionToken));
            Assert.That(result.RedirectFlow.SuccessRedirectUrl, Is.EqualTo(createResult.RedirectFlow.SuccessRedirectUrl));
        }

        [Test, Explicit("Requires user intervention to complete a session with GoCardless in a browser.")]
        public async Task CompletesRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var redirectFlow = await _resourceFactory.CreateRedirectFlowFor(creditor);
            var subject = new RedirectFlowsClient(_configuration);

            var request = new CompleteRedirectFlowRequest
            {
                Id = redirectFlow.Id,
                SessionToken = redirectFlow.SessionToken
            };

            // when
            var completedResult = await subject.CompleteAsync(request);
            var actualCompleted = completedResult.RedirectFlow;

            var result = await subject.ForIdAsync(redirectFlow.Id);
            var actual = result.RedirectFlow;

            // then;
            Assert.That(actualCompleted, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);

            Assert.That(actualCompleted.Id, Is.Not.Null.And.EqualTo(redirectFlow.Id));
            Assert.That(actualCompleted.ConfirmationUrl, Is.Not.Null.And.Contains(redirectFlow.RedirectUrl));
            Assert.That(actualCompleted.CreatedAt, Is.Not.Null.And.EqualTo(redirectFlow.CreatedAt));
            Assert.That(actualCompleted.Description, Is.EqualTo(redirectFlow.Description));
            Assert.That(actualCompleted.Links, Is.Not.Null);
            Assert.That(actualCompleted.Links.Creditor, Is.EqualTo(redirectFlow.Links.Creditor));
            Assert.That(actualCompleted.Links.Customer, Is.Not.Null);
            Assert.That(actualCompleted.Links.CustomerBankAccount, Is.Not.Null);
            Assert.That(actualCompleted.Links.Mandate, Is.Not.Null);
            Assert.That(actualCompleted.RedirectUrl, Is.Null);
            Assert.That(actualCompleted.Scheme, Is.EqualTo(redirectFlow.Scheme));
            Assert.That(actualCompleted.SessionToken, Is.EqualTo(redirectFlow.SessionToken));
            Assert.That(actualCompleted.SuccessRedirectUrl, Is.EqualTo(redirectFlow.SuccessRedirectUrl));

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(actualCompleted.Id));
            Assert.That(actual.ConfirmationUrl, Is.EqualTo(actualCompleted.ConfirmationUrl));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(actualCompleted.CreatedAt));
            Assert.That(actual.Description, Is.EqualTo(actualCompleted.Description));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.EqualTo(actualCompleted.Links.Creditor));
            Assert.That(actual.Links.Customer, Is.EqualTo(actualCompleted.Links.Customer));
            Assert.That(actual.Links.CustomerBankAccount, Is.EqualTo(actualCompleted.Links.CustomerBankAccount));
            Assert.That(actual.Links.Mandate, Is.EqualTo(actualCompleted.Links.Mandate));
            Assert.That(actual.RedirectUrl, Is.Null);
            Assert.That(actual.Scheme, Is.EqualTo(actualCompleted.Scheme));
            Assert.That(actual.SessionToken, Is.EqualTo(actualCompleted.SessionToken));
            Assert.That(actual.SuccessRedirectUrl, Is.EqualTo(actualCompleted.SuccessRedirectUrl));
        }
    }
}