using GoCardlessApi.Common;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class RedirectFlowsClientTests : IntegrationTest
    {
        private IRedirectFlowsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new RedirectFlowsClient(_configuration);
        }

        [Test]
        public async Task CreatesAndReturnsRedirectFlow()
        {
            // given
            var creditor = await _resourceFactory.Creditor();

            var createOptions = new CreateRedirectFlowOptions
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
                    CountryCode = "NZ",
                    DanishIdentityNumber = "2205506218",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "en",
                    PhoneNumber = "+44 1234 567890",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                    SwedishIdentityNumber = "5302256218",
                },
                Scheme = Scheme.BecsNz,
                SessionToken = Guid.NewGuid().ToString(),
                SuccessRedirectUrl = "https://localhost",
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);
            var result = await _subject.ForIdAsync(createResult.Item.Id);

            // then
            Assert.That(createResult.Item.ConfirmationUrl, Is.Null);
            Assert.That(createResult.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Description, Is.EqualTo(createOptions.Description));
            Assert.That(createResult.Item.Links, Is.Not.Null);
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(createOptions.Links.Creditor));
            Assert.That(createResult.Item.Links.Customer, Is.Null);
            Assert.That(createResult.Item.Links.CustomerBankAccount, Is.Null);
            Assert.That(createResult.Item.RedirectUrl, Is.Not.Null);
            Assert.That(createResult.Item.Scheme, Is.EqualTo(createOptions.Scheme));
            Assert.That(createResult.Item.SessionToken, Is.EqualTo(createOptions.SessionToken));
            Assert.That(createResult.Item.SuccessRedirectUrl, Is.EqualTo(createOptions.SuccessRedirectUrl));

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

            var options = new CompleteRedirectFlowOptions
            {
                Id = redirectFlow.Id,
                SessionToken = redirectFlow.SessionToken
            };

            // when
            var completeResult = await _subject.CompleteAsync(options);
            var actualComplete = completeResult.Item;

            var result = await _subject.ForIdAsync(redirectFlow.Id);
            var actual = result.Item;

            // then;
            Assert.That(actualComplete, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);

            Assert.That(actualComplete.Id, Is.Not.Null.And.EqualTo(redirectFlow.Id));
            Assert.That(actualComplete.ConfirmationUrl, Is.Not.Null.And.Contains(redirectFlow.RedirectUrl));
            Assert.That(actualComplete.CreatedAt, Is.Not.Null.And.EqualTo(redirectFlow.CreatedAt));
            Assert.That(actualComplete.Description, Is.EqualTo(redirectFlow.Description));
            Assert.That(actualComplete.Links, Is.Not.Null);
            Assert.That(actualComplete.Links.Creditor, Is.EqualTo(redirectFlow.Links.Creditor));
            Assert.That(actualComplete.Links.Customer, Is.Not.Null);
            Assert.That(actualComplete.Links.CustomerBankAccount, Is.Not.Null);
            Assert.That(actualComplete.Links.Mandate, Is.Not.Null);
            Assert.That(actualComplete.RedirectUrl, Is.Null);
            Assert.That(actualComplete.Scheme, Is.EqualTo(redirectFlow.Scheme));
            Assert.That(actualComplete.SessionToken, Is.EqualTo(redirectFlow.SessionToken));
            Assert.That(actualComplete.SuccessRedirectUrl, Is.EqualTo(redirectFlow.SuccessRedirectUrl));

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(actualComplete.Id));
            Assert.That(actual.ConfirmationUrl, Is.EqualTo(actualComplete.ConfirmationUrl));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(actualComplete.CreatedAt));
            Assert.That(actual.Description, Is.EqualTo(actualComplete.Description));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.EqualTo(actualComplete.Links.Creditor));
            Assert.That(actual.Links.Customer, Is.EqualTo(actualComplete.Links.Customer));
            Assert.That(actual.Links.CustomerBankAccount, Is.EqualTo(actualComplete.Links.CustomerBankAccount));
            Assert.That(actual.Links.Mandate, Is.EqualTo(actualComplete.Links.Mandate));
            Assert.That(actual.RedirectUrl, Is.Null);
            Assert.That(actual.Scheme, Is.EqualTo(actualComplete.Scheme));
            Assert.That(actual.SessionToken, Is.EqualTo(actualComplete.SessionToken));
            Assert.That(actual.SuccessRedirectUrl, Is.EqualTo(actualComplete.SuccessRedirectUrl));
        }
    }
}