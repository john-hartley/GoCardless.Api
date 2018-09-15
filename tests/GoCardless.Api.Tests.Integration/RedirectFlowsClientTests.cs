using GoCardless.Api.RedirectFlows;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class RedirectFlowsClientTests : IntegrationTest
    {
        [Test]
        public async Task CreatesAndReturnsRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var subject = new RedirectFlowsClient(_clientConfiguration);

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
            var result = await subject.ForIdAsync(createResult.Item.Id);

            // then
            Assert.That(createResult.Item.ConfirmationUrl, Is.Null);
            Assert.That(createResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Description, Is.EqualTo(createRequest.Description));
            Assert.That(createResult.Item.Links, Is.Not.Null);
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(createRequest.Links.Creditor));
            Assert.That(createResult.Item.Links.Customer, Is.Null);
            Assert.That(createResult.Item.Links.CustomerBankAccount, Is.Null);
            Assert.That(createResult.Item.RedirectUrl, Is.Not.Null);
            Assert.That(createResult.Item.Scheme, Is.EqualTo(createRequest.Scheme));
            Assert.That(createResult.Item.SessionToken, Is.EqualTo(createRequest.SessionToken));
            Assert.That(createResult.Item.SuccessRedirectUrl, Is.EqualTo(createRequest.SuccessRedirectUrl));

            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(createResult.Item.Id));
            Assert.That(result.Item.ConfirmationUrl, Is.Null);
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(createResult.Item.Id));
            Assert.That(result.Item.Description, Is.EqualTo(createResult.Item.Description));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Creditor, Is.EqualTo(createResult.Item.Links.Creditor));
            Assert.That(result.Item.Links.Customer, Is.Null);
            Assert.That(result.Item.Links.CustomerBankAccount, Is.Null);
            Assert.That(result.Item.Links.Mandate, Is.Null);
            Assert.That(result.Item.RedirectUrl, Is.Not.Null.And.EqualTo(createResult.Item.RedirectUrl));
            Assert.That(result.Item.Scheme, Is.EqualTo(createResult.Item.Scheme));
            Assert.That(result.Item.SessionToken, Is.EqualTo(createResult.Item.SessionToken));
            Assert.That(result.Item.SuccessRedirectUrl, Is.EqualTo(createResult.Item.SuccessRedirectUrl));
        }

        [Test, Explicit("Requires user intervention to complete a session with GoCardless in a browser.")]
        public async Task CompletesRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var redirectFlow = await _resourceFactory.CreateRedirectFlowFor(creditor);
            var subject = new RedirectFlowsClient(_clientConfiguration);

            var request = new CompleteRedirectFlowRequest
            {
                Id = redirectFlow.Id,
                SessionToken = redirectFlow.SessionToken
            };

            // when
            var completedResult = await subject.CompleteAsync(request);
            var actualCompleted = completedResult.Item;

            var result = await subject.ForIdAsync(redirectFlow.Id);
            var actual = result.Item;

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