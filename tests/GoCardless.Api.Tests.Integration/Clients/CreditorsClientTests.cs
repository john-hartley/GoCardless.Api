using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using GoCardlessApi.Creditors;
using GoCardlessApi.Tests.Integration.TestHelpers;
using GoCardlessApi.Common;

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
        public async Task returns_creditors()
        {
            // given
            var creditor = await _resourceFactory.Creditor();

            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Validate(result[0], creditor);

            var schemeIdentifier = result[0].SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Validate(schemeIdentifier);
        }

        [Test]
        public async Task maps_paging_properties()
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
        public async Task returns_creditor()
        {
            // given
            var creditor = await _resourceFactory.Creditor();

            // when
            var result = await _subject.ForIdAsync(creditor.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Validate(actual, creditor);

            var schemeIdentifier = actual.SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Validate(schemeIdentifier);

            var sepaSchemeIdentifier = actual.SchemeIdentifiers.SingleOrDefault(x => x.Scheme == Scheme.Sepa);
            Assert.That(sepaSchemeIdentifier, Is.Not.Null);
        }

        [Test, Explicit("Can fail if a bank account is set to disabled by another test after the creditor was fetched.")]
        public async Task updates_creditor()
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
                    DefaultAudPayoutAccount = creditor.Links.DefaultAudPayoutAccount,
                    DefaultCadPayoutAccount = creditor.Links.DefaultCadPayoutAccount,
                    DefaultDkkPayoutAccount = creditor.Links.DefaultDkkPayoutAccount,
                    DefaultEurPayoutAccount = creditor.Links.DefaultEurPayoutAccount,
                    DefaultGbpPayoutAccount = creditor.Links.DefaultGbpPayoutAccount,
                    DefaultNzdPayoutAccount = creditor.Links.DefaultNzdPayoutAccount,
                    DefaultSekPayoutAccount = creditor.Links.DefaultSekPayoutAccount,
                    DefaultUsdPayoutAccount = creditor.Links.DefaultUsdPayoutAccount,
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
            Assert.That(actual.Links.DefaultAudPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultAudPayoutAccount));
            Assert.That(actual.Links.DefaultCadPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultCadPayoutAccount));
            Assert.That(actual.Links.DefaultDkkPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultDkkPayoutAccount));
            Assert.That(actual.Links.DefaultEurPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultEurPayoutAccount));
            Assert.That(actual.Links.DefaultGbpPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultGbpPayoutAccount));
            Assert.That(actual.Links.DefaultNzdPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultNzdPayoutAccount));
            Assert.That(actual.Links.DefaultSekPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultSekPayoutAccount));
            Assert.That(actual.Links.DefaultUsdPayoutAccount, Is.Not.Null.And.EqualTo(creditor.Links.DefaultUsdPayoutAccount));
            Assert.That(actual.Name, Is.EqualTo(options.Name));
            Assert.That(actual.PostalCode, Is.EqualTo(options.PostalCode));
            Assert.That(actual.Region, Is.EqualTo(options.Region));
            Assert.That(actual.SchemeIdentifiers, Is.Not.Null);
            Assert.That(actual.SchemeIdentifiers.Any(), Is.True);
            Assert.That(actual.VerificationStatus, Is.Not.Null);

            var schemeIdentifier = actual.SchemeIdentifiers.SingleOrDefault(x => x.Currency == "GBP");
            Validate(schemeIdentifier);
        }

        private static void Validate(Creditor actual, Creditor expected)
        {
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(expected.Id));
            Assert.That(actual.AddressLine1, Is.Not.Null.And.EqualTo(expected.AddressLine1));
            Assert.That(actual.AddressLine2, Is.Not.Null.And.EqualTo(expected.AddressLine2));
            Assert.That(actual.AddressLine3, Is.Not.Null.And.EqualTo(expected.AddressLine3));
            Assert.That(actual.CanCreateRefunds, Is.Not.Null.And.EqualTo(expected.CanCreateRefunds));
            Assert.That(actual.City, Is.Not.Null.And.EqualTo(expected.City));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(expected.CountryCode));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(expected.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.DefaultAudPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultAudPayoutAccount));
            Assert.That(actual.Links.DefaultCadPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultCadPayoutAccount));
            Assert.That(actual.Links.DefaultDkkPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultDkkPayoutAccount));
            Assert.That(actual.Links.DefaultEurPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultEurPayoutAccount));
            Assert.That(actual.Links.DefaultGbpPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultGbpPayoutAccount));
            Assert.That(actual.Links.DefaultNzdPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultNzdPayoutAccount));
            Assert.That(actual.Links.DefaultSekPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultSekPayoutAccount));
            Assert.That(actual.Links.DefaultUsdPayoutAccount, Is.Not.Null.And.EqualTo(expected.Links.DefaultUsdPayoutAccount));
            Assert.That(actual.Name, Is.Not.Null.And.EqualTo(expected.Name));
            Assert.That(actual.PostalCode, Is.Not.Null.And.EqualTo(expected.PostalCode));
            Assert.That(actual.Region, Is.Not.Null.And.EqualTo(expected.Region));
            Assert.That(actual.SchemeIdentifiers, Is.Not.Null);
            Assert.That(actual.SchemeIdentifiers.Any(), Is.True);
            Assert.That(actual.VerificationStatus, Is.Not.Null.And.EqualTo(expected.VerificationStatus));
        }

        private static void Validate(SchemeIdentifier actual)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.AddressLine1, Is.Not.Null);
            Assert.That(actual.CanSpecifyMandateReference, Is.True);
            Assert.That(actual.City, Is.Not.Null);
            Assert.That(actual.CountryCode, Is.Not.Null);
            Assert.That(actual.Currency, Is.Not.Null);
            Assert.That(actual.Email, Is.Not.Null);
            Assert.That(actual.MinimumAdvanceNotice, Is.Not.EqualTo(default(int)));
            Assert.That(actual.Name, Is.Not.Null);
            Assert.That(actual.PhoneNumber, Is.Not.Null);
            Assert.That(actual.PostalCode, Is.Not.Null);
            Assert.That(actual.Region, Is.Not.Null);
            Assert.That(actual.Reference, Is.Not.Null);
            Assert.That(actual.Scheme, Is.Not.Null);
        }
    }
}