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
        [Test]
        public async Task ReturnsPayouts()
        {
            // given
            var subject = new PayoutsClient(ClientConfiguration.ForSandbox(_accessToken));

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
    }
}