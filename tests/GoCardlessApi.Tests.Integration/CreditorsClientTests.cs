using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class CreditorsClientTests : IntegrationTest
    {
        [Test]
        public async Task ReturnsIndividualCreditor()
        {
            // given
            var creditorId = "CR00005N9ZWBFK";
            var expectedCreatedAt = DateTimeOffset.Parse("2018-08-20T05:53:51.860Z");

            var subject = new CreditorsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.ForIdAsync(creditorId);

            // then
            Assert.That(result.Creditor.Id, Is.EqualTo(creditorId));
            Assert.That(result.Creditor.CreatedAt, Is.EqualTo(expectedCreatedAt));
            Assert.That(result.Creditor.Name, Is.EqualTo("API Client Development"));
            Assert.That(result.Creditor.AddressLine1, Is.EqualTo("Address Line 1"));
            Assert.That(result.Creditor.AddressLine2, Is.EqualTo("Address Line 2"));
            Assert.That(result.Creditor.AddressLine3, Is.EqualTo("Address Line 3"));
            Assert.That(result.Creditor.City, Is.EqualTo("London"));
            Assert.That(result.Creditor.Region, Is.EqualTo("Essex"));
            Assert.That(result.Creditor.PostCode, Is.EqualTo("SW1A 1AA"));
            Assert.That(result.Creditor.CountryCode, Is.EqualTo("GB"));
            Assert.That(result.Creditor.LogoUrl, Is.Null);
            Assert.That(result.Creditor.VerificationStatus, Is.EqualTo("successful"));
            Assert.That(result.Creditor.CanCreateRefunds, Is.False);
        }

        [Test]
        public async Task UpdatesCreditor()
        {
            // given
            var request = new UpdateCreditorRequest
            {
                Id = "CR00005N9ZWBFK",
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CountryCode = "GB",
                Name = "API Client Development",
                PostCode = "SW1A 1AA",
                Region = "Essex"
            };

            var subject = new CreditorsClient(ClientConfiguration.ForSandbox(_accessToken));

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
            Assert.That(result.Creditor.PostCode, Is.EqualTo(request.PostCode));
            Assert.That(result.Creditor.CountryCode, Is.EqualTo(request.CountryCode));
        }
    }
}