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

            var subject = new CreditorsClient(_accessToken);

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
    }
}