using GoCardlessApi.Core;
using GoCardlessApi.Payouts;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class PayoutsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;

        public PayoutsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
        }

        [Test]
        public async Task ReturnsPayouts()
        {
            // given
            var subject = new PayoutsClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Payouts.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].ArrivalDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].DeductedFees, Is.Not.Null.And.Not.EqualTo(default(int)));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
            Assert.That(result[0].Links.CreditorBankAccount, Is.Not.Null);
            Assert.That(result[0].PayoutType, Is.Not.Null);
            Assert.That(result[0].Reference, Is.Not.Null);
            Assert.That(result[0].Status, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualPayout()
        {
            // given
            var subject = new PayoutsClient(_configuration);
            var payout = (await subject.AllAsync()).Payouts.First();

            // when
            var result = await subject.ForIdAsync(payout.Id);
            var actual = result.Payout;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(payout.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(payout.Amount));
            Assert.That(actual.ArrivalDate, Is.Not.Null.And.EqualTo(payout.ArrivalDate));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(payout.CreatedAt));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(payout.Currency));
            Assert.That(actual.DeductedFees, Is.Not.Null.And.EqualTo(payout.DeductedFees));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.Not.Null.And.EqualTo(payout.Links.Creditor));
            Assert.That(actual.Links.CreditorBankAccount, Is.Not.Null.And.EqualTo(payout.Links.CreditorBankAccount));
            Assert.That(actual.PayoutType, Is.Not.Null.And.EqualTo(payout.PayoutType));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(payout.Reference));
            Assert.That(actual.Status, Is.Not.Null.And.EqualTo(payout.Status));
        }
    }
}