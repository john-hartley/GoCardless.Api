using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using GoCardlessApi.Creditors;
using GoCardlessApi.Tests.Integration.TestHelpers;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class CreditorsClientTests : IntegrationTest
    {
        private ICreditorsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CreditorsClient(_configuration);
        }

        [Test, NonParallelizable]
        public async Task ReturnsCreditors()
        {
            // given
            var creditor = await _resourceFactory.Creditor();

            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Id, Is.Not.Null.And.EqualTo(creditor.Id));
            Assert.That(result[0].AddressLine1, Is.Not.Null.And.EqualTo(creditor.AddressLine1));
            Assert.That(result[0].AddressLine2, Is.Not.Null.And.EqualTo(creditor.AddressLine2));
            Assert.That(result[0].AddressLine3, Is.Not.Null.And.EqualTo(creditor.AddressLine3));
            Assert.That(result[0].CanCreateRefunds, Is.Not.Null.And.EqualTo(creditor.CanCreateRefunds));
            Assert.That(result[0].City, Is.Not.Null.And.EqualTo(creditor.City));
            Assert.That(result[0].CountryCode, Is.Not.Null.And.EqualTo(creditor.CountryCode));
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.EqualTo(creditor.CreatedAt));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.DefaultDkkPayoutAccount, Is.Not.Null);
            Assert.That(result[0].Links.DefaultGbpPayoutAccount, Is.Not.Null);
            Assert.That(result[0].Links.DefaultSekPayoutAccount, Is.Not.Null);
            Assert.That(result[0].Name, Is.Not.Null.And.EqualTo(creditor.Name));
            Assert.That(result[0].PostalCode, Is.Not.Null.And.EqualTo(creditor.PostalCode));
            Assert.That(result[0].Region, Is.Not.Null.And.EqualTo(creditor.Region));
            Assert.That(result[0].VerificationStatus, Is.Not.Null.And.EqualTo(creditor.VerificationStatus));

            Assert.That(result[0].SchemeIdentifiers, Is.Not.Null);
            Assert.That(result[0].SchemeIdentifiers.Any(), Is.True);

            var schemeIdentifier = result[0].SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Assert.That(schemeIdentifier, Is.Not.Null);
            Assert.That(schemeIdentifier.AddressLine1, Is.Not.Null);
            Assert.That(schemeIdentifier.CanSpecifyMandateReference, Is.True);
            Assert.That(schemeIdentifier.City, Is.Not.Null);
            Assert.That(schemeIdentifier.CountryCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Currency, Is.Not.Null);
            Assert.That(schemeIdentifier.Email, Is.Not.Null);
            Assert.That(schemeIdentifier.MinimumAdvanceNotice, Is.Not.EqualTo(default(int)));
            Assert.That(schemeIdentifier.Name, Is.Not.Null);
            Assert.That(schemeIdentifier.PhoneNumber, Is.Not.Null);
            Assert.That(schemeIdentifier.PostalCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Region, Is.Not.Null);
            Assert.That(schemeIdentifier.Reference, Is.Not.Null);
            Assert.That(schemeIdentifier.Scheme, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var options = new GetCreditorsOptions
            {
                Limit = 1
            };

            // when
            var result = await _subject.GetPageAsync(options);

            // then
            Assert.That(result.Items.Count(), Is.EqualTo(options.Limit));
            Assert.That(result.Meta.Limit, Is.EqualTo(options.Limit));
            Assert.That(result.Meta.Cursors.Before, Is.Null);
            Assert.That(result.Meta.Cursors.After, Is.Null);
        }

        [Test, NonParallelizable]
        public async Task ReturnsIndividualCreditor()
        {
            // given
            var creditor = await _resourceFactory.Creditor();

            // when
            var result = await _subject.ForIdAsync(creditor.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(creditor.Id));
            Assert.That(actual.AddressLine1, Is.Not.Null.And.EqualTo(creditor.AddressLine1));
            Assert.That(actual.AddressLine2, Is.Not.Null.And.EqualTo(creditor.AddressLine2));
            Assert.That(actual.AddressLine3, Is.Not.Null.And.EqualTo(creditor.AddressLine3));
            Assert.That(actual.CanCreateRefunds, Is.Not.Null.And.EqualTo(creditor.CanCreateRefunds));
            Assert.That(actual.City, Is.Not.Null.And.EqualTo(creditor.City));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(creditor.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(creditor.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.DefaultDkkPayoutAccount, Is.Not.Null);
            Assert.That(actual.Links.DefaultGbpPayoutAccount, Is.Not.Null);
            Assert.That(actual.Links.DefaultSekPayoutAccount, Is.Not.Null);
            Assert.That(actual.Name, Is.Not.Null.And.EqualTo(creditor.Name));
            Assert.That(actual.PostalCode, Is.Not.Null.And.EqualTo(creditor.PostalCode));
            Assert.That(actual.Region, Is.Not.Null.And.EqualTo(creditor.Region));
            Assert.That(actual.VerificationStatus, Is.Not.Null.And.EqualTo(creditor.VerificationStatus));

            Assert.That(actual.SchemeIdentifiers, Is.Not.Null);
            Assert.That(actual.SchemeIdentifiers.Any(), Is.True);

            var schemeIdentifier = actual.SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Assert.That(schemeIdentifier, Is.Not.Null);
            Assert.That(schemeIdentifier.AddressLine1, Is.Not.Null);
            Assert.That(schemeIdentifier.CanSpecifyMandateReference, Is.True);
            Assert.That(schemeIdentifier.City, Is.Not.Null);
            Assert.That(schemeIdentifier.CountryCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Currency, Is.Not.Null);
            Assert.That(schemeIdentifier.Email, Is.Not.Null);
            Assert.That(schemeIdentifier.MinimumAdvanceNotice, Is.Not.EqualTo(default(int)));
            Assert.That(schemeIdentifier.Name, Is.Not.Null);
            Assert.That(schemeIdentifier.PhoneNumber, Is.Not.Null);
            Assert.That(schemeIdentifier.PostalCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Region, Is.Not.Null);
            Assert.That(schemeIdentifier.Reference, Is.Not.Null);
            Assert.That(schemeIdentifier.Scheme, Is.Not.Null);
        }

        [Test, NonParallelizable]
        public async Task UpdatesCreditor()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var options = new UpdateCreditorOptions
            {
                Id = creditor.Id,
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CountryCode = "GB",
                Links = new CreditorLinks
                {
                    DefaultDkkPayoutAccount = creditor.Links.DefaultDkkPayoutAccount,
                    DefaultGbpPayoutAccount = creditor.Links.DefaultGbpPayoutAccount,
                    DefaultSekPayoutAccount = creditor.Links.DefaultSekPayoutAccount,
                },
                Name = "API Client Development",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(options.Id));
            Assert.That(actual.AddressLine1, Is.EqualTo(options.AddressLine1));
            Assert.That(actual.AddressLine2, Is.EqualTo(options.AddressLine2));
            Assert.That(actual.AddressLine3, Is.EqualTo(options.AddressLine3));
            Assert.That(actual.CanCreateRefunds, Is.Not.Null);
            Assert.That(actual.City, Is.EqualTo(options.City));
            Assert.That(actual.CountryCode, Is.EqualTo(options.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(creditor.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.DefaultDkkPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultDkkPayoutAccount));
            Assert.That(actual.Links.DefaultGbpPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultGbpPayoutAccount));
            Assert.That(actual.Links.DefaultSekPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultSekPayoutAccount));
            Assert.That(actual.Name, Is.EqualTo(options.Name));
            Assert.That(actual.PostalCode, Is.EqualTo(options.PostalCode));
            Assert.That(actual.Region, Is.EqualTo(options.Region));
            Assert.That(actual.VerificationStatus, Is.Not.Null);

            Assert.That(actual.SchemeIdentifiers, Is.Not.Null);
            Assert.That(actual.SchemeIdentifiers.Any(), Is.True);

            var schemeIdentifier = actual.SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Assert.That(schemeIdentifier, Is.Not.Null);
            Assert.That(schemeIdentifier.AddressLine1, Is.Not.Null);
            Assert.That(schemeIdentifier.CanSpecifyMandateReference, Is.True);
            Assert.That(schemeIdentifier.City, Is.Not.Null);
            Assert.That(schemeIdentifier.CountryCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Currency, Is.Not.Null);
            Assert.That(schemeIdentifier.Email, Is.Not.Null);
            Assert.That(schemeIdentifier.MinimumAdvanceNotice, Is.Not.EqualTo(default(int)));
            Assert.That(schemeIdentifier.Name, Is.Not.Null);
            Assert.That(schemeIdentifier.PhoneNumber, Is.Not.Null);
            Assert.That(schemeIdentifier.PostalCode, Is.Not.Null);
            Assert.That(schemeIdentifier.Region, Is.Not.Null);
            Assert.That(schemeIdentifier.Reference, Is.Not.Null);
            Assert.That(schemeIdentifier.Scheme, Is.Not.Null);
        }
    }
}