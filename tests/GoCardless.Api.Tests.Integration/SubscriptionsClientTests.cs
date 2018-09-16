using GoCardless.Api.Mandates;
using GoCardless.Api.Subscriptions;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class SubscriptionsClientTests : IntegrationTest
    {
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customer, customerBankAccount);
        }

        [Test]
        public async Task CreatesSubscription()
        {
            // given
            var request = new CreateSubscriptionRequest
            {
                Amount = 123,
                Currency = "GBP",
                IntervalUnit = "weekly",
                //AppFee = 12,
                Count = 5,
                //DayOfMonth = 17,
                //EndDate = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd"),
                Interval = 1,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                //Month = "september",
                Name = "Test subscription",
                PaymentReference = "PR123456",
                StartDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                }
            };
            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.Item.Id, Is.Not.Empty);
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Item.Currency, Is.EqualTo(request.Currency));
            Assert.That(result.Item.Status, Is.EqualTo("active"));
            Assert.That(result.Item.Name, Is.EqualTo(request.Name));
            Assert.That(result.Item.StartDate.ToString("yyyy-MM-dd"), Is.EqualTo(request.StartDate));
            //Assert.That(result.Item.EndDate, Is.EqualTo(request.EndDate));
            Assert.That(result.Item.Interval, Is.EqualTo(request.Interval));
            Assert.That(result.Item.IntervalUnit, Is.EqualTo(request.IntervalUnit));
            Assert.That(result.Item.DayOfMonth, Is.EqualTo(request.DayOfMonth));
            Assert.That(result.Item.Month, Is.EqualTo(request.Month));
            Assert.That(result.Item.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(request.PaymentReference));
            Assert.That(result.Item.UpcomingPayments.Count(), Is.EqualTo(request.Count));
            Assert.That(result.Item.AppFee, Is.EqualTo(request.AppFee));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.EqualTo(request.Links.Mandate));
        }

        [Test]
        public async Task ReturnsAllSubscriptions()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.GetPageAsync();
            var actual = result.Items.ToList();

            // then
            Assert.That(actual.Any(), Is.True);
            Assert.That(result.Meta.Limit, Is.Not.EqualTo(0));
            Assert.That(actual.Count, Is.LessThanOrEqualTo(result.Meta.Limit));
            Assert.That(actual[0], Is.Not.Null);
            Assert.That(actual[0].Id, Is.Not.Null);
            Assert.That(actual[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(actual[0].AppFee, Is.Null);
            Assert.That(actual[0].Currency, Is.Not.Null);
            Assert.That(actual[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual[0].DayOfMonth, Is.Null);
            Assert.That(actual[0].EndDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(actual[0].Interval, Is.Not.Null);
            Assert.That(actual[0].IntervalUnit, Is.Not.Null);
            Assert.That(actual[0].Links, Is.Not.Null);
            Assert.That(actual[0].Links.Mandate, Is.Not.Null);
            Assert.That(actual[0].Metadata, Is.Not.Null);
            Assert.That(actual[0].Month, Is.Null);
            Assert.That(actual[0].Name, Is.Not.Null);
            Assert.That(actual[0].PaymentReference, Is.Not.Null);
            Assert.That(actual[0].StartDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(actual[0].Status, Is.Not.Null);
            Assert.That(actual[0].UpcomingPayments, Is.Not.Null);
            Assert.That(actual[0].UpcomingPayments.Any(), Is.True);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);

            var firstPageRequest = new GetSubscriptionsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetSubscriptionsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.GetPageAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualSubscription()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.ForIdAsync(subscription.Id);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(subscription.Id));
            Assert.That(result.Item.Amount, Is.Not.Null.And.EqualTo(subscription.Amount));
            Assert.That(result.Item.AppFee, Is.Null);
            Assert.That(result.Item.Currency, Is.Not.Null.And.EqualTo(subscription.Currency));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.DayOfMonth, Is.Null);
            Assert.That(result.Item.EndDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result.Item.Interval, Is.Not.Null.And.EqualTo(subscription.Interval));
            Assert.That(result.Item.IntervalUnit, Is.Not.Null.And.EqualTo(subscription.IntervalUnit));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.Not.Null.And.EqualTo(subscription.Links.Mandate));
            Assert.That(result.Item.Metadata, Is.Not.Null.And.EqualTo(subscription.Metadata));
            Assert.That(result.Item.Month, Is.Null);
            Assert.That(result.Item.Name, Is.Not.Null.And.EqualTo(subscription.Name));
            Assert.That(result.Item.PaymentReference, Is.Not.Null.And.EqualTo(subscription.PaymentReference));
            Assert.That(result.Item.StartDate, Is.Not.Null.And.EqualTo(subscription.StartDate));
            Assert.That(result.Item.Status, Is.Not.Null.And.EqualTo(subscription.Status));
            Assert.That(result.Item.UpcomingPayments, Is.Not.Null);
            Assert.That(result.Item.UpcomingPayments.Count(), Is.EqualTo(subscription.UpcomingPayments.Count()));
        }

        [Test]
        public async Task UpdatesSubscriptionPreservingMetadata()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var request = new UpdateSubscriptionRequest
            {
                Id = subscription.Id,
                Amount = 456,
                Name = "Updated subscription name",
                PaymentReference = "PR456789"
            };

            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.UpdateAsync(request);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(request.Id));
            Assert.That(result.Item.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Item.AppFee, Is.Null);
            Assert.That(result.Item.Metadata, Is.EqualTo(subscription.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(request.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(request.PaymentReference));
        }

        [Test]
        public async Task UpdatesSubscriptionReplacingMetadata()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var request = new UpdateSubscriptionRequest
            {
                Id = subscription.Id,
                Amount = 456,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
                Name = "Updated subscription name",
                PaymentReference = "PR456789"
            };

            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.UpdateAsync(request);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(request.Id));
            Assert.That(result.Item.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Item.AppFee, Is.Null);
            Assert.That(result.Item.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(request.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(request.PaymentReference));
        }

        [Test]
        public async Task CancelsSubscription()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var request = new CancelSubscriptionRequest
            {
                Id = subscription.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var subject = new SubscriptionsClient(_clientConfiguration);

            // when
            var result = await subject.CancelAsync(request);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(request.Id));
            Assert.That(result.Item.Status, Is.EqualTo("cancelled"));
        }

        [Test, Explicit("Can end up performing 1 <= 49 calls.")]
        public async Task PagesThroughSubscriptions()
        {
            // given
            var subject = new SubscriptionsClient(_clientConfiguration);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetSubscriptionsRequest
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1)),
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}