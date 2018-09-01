using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using GoCardlessApi.Subscriptions;
using GoCardlessApi.Core;
using GoCardlessApi.Tests.Integration.TestHelpers;
using GoCardlessApi.Mandates;

namespace GoCardlessApi.Tests.Integration
{
    public class SubscriptionsClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Mandate _mandate;

        public SubscriptionsClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

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
                Interval = 3,
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
            var subject = new SubscriptionsClient(_configuration);

            // when
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.Not.Empty);
            Assert.That(result.Subscription.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Subscription.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Subscription.Currency, Is.EqualTo(request.Currency));
            Assert.That(result.Subscription.Status, Is.EqualTo("active"));
            Assert.That(result.Subscription.Name, Is.EqualTo(request.Name));
            Assert.That(result.Subscription.StartDate.ToString("yyyy-MM-dd"), Is.EqualTo(request.StartDate));
            //Assert.That(result.Subscription.EndDate, Is.EqualTo(request.EndDate));
            Assert.That(result.Subscription.Interval, Is.EqualTo(request.Interval));
            Assert.That(result.Subscription.IntervalUnit, Is.EqualTo(request.IntervalUnit));
            Assert.That(result.Subscription.DayOfMonth, Is.EqualTo(request.DayOfMonth));
            Assert.That(result.Subscription.Month, Is.EqualTo(request.Month));
            Assert.That(result.Subscription.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Subscription.PaymentReference, Is.EqualTo(request.PaymentReference));
            Assert.That(result.Subscription.UpcomingPayments.Count(), Is.EqualTo(request.Count));
            Assert.That(result.Subscription.AppFee, Is.EqualTo(request.AppFee));
            Assert.That(result.Subscription.Links, Is.Not.Null);
            Assert.That(result.Subscription.Links.Mandate, Is.EqualTo(request.Links.Mandate));
        }

        [Test]
        public async Task ReturnsAllSubscriptions()
        {
            // given
            var subject = new SubscriptionsClient(_configuration);

            // when
            var result = await subject.AllAsync();
            var actual = result.Subscriptions.ToList();

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
            var subject = new SubscriptionsClient(_configuration);

            var firstPageRequest = new AllSubscriptionsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.AllAsync(firstPageRequest);

            var secondPageRequest = new AllSubscriptionsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.AllAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(firstPageResult.Subscriptions.Count(), Is.EqualTo(firstPageRequest.Limit));

            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(secondPageResult.Subscriptions.Count(), Is.EqualTo(secondPageRequest.Limit));
        }

        [Test]
        public async Task ReturnsIndividualSubscription()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var subject = new SubscriptionsClient(_configuration);

            // when
            var result = await subject.ForIdAsync(subscription.Id);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(subscription.Id));
            Assert.That(result.Subscription.Amount, Is.Not.Null.And.EqualTo(subscription.Amount));
            Assert.That(result.Subscription.AppFee, Is.Null);
            Assert.That(result.Subscription.Currency, Is.Not.Null.And.EqualTo(subscription.Currency));
            Assert.That(result.Subscription.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Subscription.DayOfMonth, Is.Null);
            Assert.That(result.Subscription.EndDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result.Subscription.Interval, Is.Not.Null.And.EqualTo(subscription.Interval));
            Assert.That(result.Subscription.IntervalUnit, Is.Not.Null.And.EqualTo(subscription.IntervalUnit));
            Assert.That(result.Subscription.Links, Is.Not.Null);
            Assert.That(result.Subscription.Links.Mandate, Is.Not.Null.And.EqualTo(subscription.Links.Mandate));
            Assert.That(result.Subscription.Metadata, Is.Not.Null.And.EqualTo(subscription.Metadata));
            Assert.That(result.Subscription.Month, Is.Null);
            Assert.That(result.Subscription.Name, Is.Not.Null.And.EqualTo(subscription.Name));
            Assert.That(result.Subscription.PaymentReference, Is.Not.Null.And.EqualTo(subscription.PaymentReference));
            Assert.That(result.Subscription.StartDate, Is.Not.Null.And.EqualTo(subscription.StartDate));
            Assert.That(result.Subscription.Status, Is.Not.Null.And.EqualTo(subscription.Status));
            Assert.That(result.Subscription.UpcomingPayments, Is.Not.Null);
            Assert.That(result.Subscription.UpcomingPayments.Count(), Is.EqualTo(subscription.UpcomingPayments.Count()));
        }

        [Test]
        public async Task UpdatesSubscription()
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

            var subject = new SubscriptionsClient(_configuration);

            // when
            var result = await subject.UpdateAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(request.Id));
            Assert.That(result.Subscription.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Subscription.AppFee, Is.Null);
            Assert.That(result.Subscription.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Subscription.Name, Is.EqualTo(request.Name));
            Assert.That(result.Subscription.PaymentReference, Is.EqualTo(request.PaymentReference));
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

            var subject = new SubscriptionsClient(_configuration);

            // when
            var result = await subject.CancelAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(request.Id));
            Assert.That(result.Subscription.Status, Is.EqualTo("cancelled"));
        }
    }
}