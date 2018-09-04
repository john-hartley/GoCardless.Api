using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using GoCardlessApi.Creditors;
using GoCardlessApi.Core;
using GoCardlessApi.Tests.Integration.TestHelpers;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        public CreditorsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [Test, NonParallelizable]
        public async Task ReturnsCreditors()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var subject = new CreditorsClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Creditors.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0].Id, Is.Not.Null.And.EqualTo(creditor.Id));
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.EqualTo(creditor.CreatedAt));
            Assert.That(result[0].Name, Is.Not.Null.And.EqualTo(creditor.Name));
            Assert.That(result[0].AddressLine1, Is.Not.Null.And.EqualTo(creditor.AddressLine1));
            Assert.That(result[0].AddressLine2, Is.Not.Null.And.EqualTo(creditor.AddressLine2));
            Assert.That(result[0].AddressLine3, Is.Not.Null.And.EqualTo(creditor.AddressLine3));
            Assert.That(result[0].City, Is.Not.Null.And.EqualTo(creditor.City));
            Assert.That(result[0].Region, Is.Not.Null.And.EqualTo(creditor.Region));
            Assert.That(result[0].PostalCode, Is.Not.Null.And.EqualTo(creditor.PostalCode));
            Assert.That(result[0].CountryCode, Is.Not.Null.And.EqualTo(creditor.CountryCode));
            Assert.That(result[0].VerificationStatus, Is.Not.Null.And.EqualTo(creditor.VerificationStatus));
            Assert.That(result[0].CanCreateRefunds, Is.Not.Null.And.EqualTo(creditor.CanCreateRefunds));
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new CreditorsClient(_configuration);

            var request = new AllCreditorsRequest
            {
                Limit = 1
            };

            // when
            var result = await subject.AllAsync(request);

            // then
            Assert.That(result.Meta.Limit, Is.EqualTo(request.Limit));
            Assert.That(result.Meta.Cursors.Before, Is.Null);
            Assert.That(result.Meta.Cursors.After, Is.Null);
            Assert.That(result.Creditors.Count(), Is.EqualTo(request.Limit));
        }

        [Test, NonParallelizable]
        public async Task ReturnsIndividualCreditor()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var subject = new CreditorsClient(_configuration);

            // when
            var result = await subject.ForIdAsync(creditor.Id);
            var actual = result.Creditor;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(creditor.Id));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(creditor.CreatedAt));
            Assert.That(actual.Name, Is.Not.Null.And.EqualTo(creditor.Name));
            Assert.That(actual.AddressLine1, Is.Not.Null.And.EqualTo(creditor.AddressLine1));
            Assert.That(actual.AddressLine2, Is.Not.Null.And.EqualTo(creditor.AddressLine2));
            Assert.That(actual.AddressLine3, Is.Not.Null.And.EqualTo(creditor.AddressLine3));
            Assert.That(actual.City, Is.Not.Null.And.EqualTo(creditor.City));
            Assert.That(actual.Region, Is.Not.Null.And.EqualTo(creditor.Region));
            Assert.That(actual.PostalCode, Is.Not.Null.And.EqualTo(creditor.PostalCode));
            Assert.That(actual.CountryCode, Is.Not.Null.And.EqualTo(creditor.CountryCode));
            Assert.That(actual.VerificationStatus, Is.Not.Null.And.EqualTo(creditor.VerificationStatus));
            Assert.That(actual.CanCreateRefunds, Is.Not.Null.And.EqualTo(creditor.CanCreateRefunds));
        }

        [Test, NonParallelizable]
        public async Task UpdatesCreditor()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var request = new UpdateCreditorRequest
            {
                Id = creditor.Id,
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CountryCode = "GB",
                Name = "API Client Development",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
            };

            var subject = new CreditorsClient(_configuration);

            // when
            var result = await subject.UpdateAsync(request);

            // then
            Assert.That(result.Creditor.Id, Is.EqualTo(request.Id));
            Assert.That(result.Creditor.Name, Is.EqualTo(request.Name));
            Assert.That(result.Creditor.AddressLine1, Is.EqualTo(request.AddressLine1));
            Assert.That(result.Creditor.AddressLine2, Is.EqualTo(request.AddressLine2));
            Assert.That(result.Creditor.AddressLine3, Is.EqualTo(request.AddressLine3));
            Assert.That(result.Creditor.City, Is.EqualTo(request.City));
            Assert.That(result.Creditor.Region, Is.EqualTo(request.Region));
            Assert.That(result.Creditor.PostalCode, Is.EqualTo(request.PostalCode));
            Assert.That(result.Creditor.CountryCode, Is.EqualTo(request.CountryCode));
            Assert.That(result.Creditor.VerificationStatus, Is.Not.Null);
            Assert.That(result.Creditor.CanCreateRefunds, Is.Not.Null);
        }
    }
}