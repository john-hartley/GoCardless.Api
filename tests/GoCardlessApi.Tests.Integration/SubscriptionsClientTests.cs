using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace GoCardlessApi.Tests.Integration
{
    public class SubscriptionsClientTests : IntegrationTest
    {
        [Test]
        public async Task ReturnsAllSubscriptions()
        {
            // given
            var subject = new SubscriptionsClient(_accessToken);

            // when
            var result = await subject.AllAsync();

            // then
            Assert.That(result.Subscriptions.Any(), Is.True);
        }

        [Test]
        public async Task ReturnsIndividualSubscription()
        {
            // given
            var subscriptionId = "SB0000G9PZP814";
            var expectedCreatedAt = DateTimeOffset.Parse("2018-07-30T19:28:02.962Z");
            var expectedStartDate = DateTime.Parse("2018-08-06");

            var subject = new SubscriptionsClient(_accessToken);

            // when
            var result = await subject.ForIdAsync(subscriptionId);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(subscriptionId));
            Assert.That(result.Subscription.CreatedAt, Is.EqualTo(expectedCreatedAt));
            Assert.That(result.Subscription.Amount, Is.EqualTo(500));
            Assert.That(result.Subscription.Currency, Is.EqualTo("GBP"));
            Assert.That(result.Subscription.Status, Is.EqualTo("active"));
            Assert.That(result.Subscription.Name, Is.Null);
            Assert.That(result.Subscription.StartDate, Is.EqualTo(expectedStartDate));
            Assert.That(result.Subscription.EndDate, Is.Null);
            Assert.That(result.Subscription.Interval, Is.EqualTo(1));
            Assert.That(result.Subscription.IntervalUnit, Is.EqualTo("monthly"));
            Assert.That(result.Subscription.DayOfMonth, Is.EqualTo(6));
            Assert.That(result.Subscription.Month, Is.Null);
            Assert.That(result.Subscription.Metadata, Is.Empty);
            Assert.That(result.Subscription.PaymentReference, Is.Null);
            Assert.That(result.Subscription.UpcomingPayments.Any(), Is.True);
            Assert.That(result.Subscription.AppFee, Is.EqualTo(0));
            Assert.That(result.Subscription.Links, Is.Not.Null);
            Assert.That(result.Subscription.Links.Mandate, Is.EqualTo("MD0003T17KJWM8"));
        }
    }
}