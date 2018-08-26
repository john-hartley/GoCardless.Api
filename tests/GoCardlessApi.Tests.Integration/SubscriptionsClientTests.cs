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
                //PaymentReference = "PaymentReference123",
                StartDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Links = new Links
                {
                    Mandate = _mandate.Id
                }
            };
            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.Not.Empty);
            //Assert.That(result.Subscription.CreatedAt, Is.EqualTo(expectedCreatedAt));
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
            //Assert.That(result.Subscription.PaymentReference, Is.EqualTo(request.PaymentReference));
            Assert.That(result.Subscription.UpcomingPayments.Count(), Is.EqualTo(request.Count));
            Assert.That(result.Subscription.AppFee, Is.EqualTo(request.AppFee));
            Assert.That(result.Subscription.Links, Is.Not.Null);
            Assert.That(result.Subscription.Links.Mandate, Is.EqualTo(request.Links.Mandate));
        }

        [Test]
        public async Task ReturnsAllSubscriptions()
        {
            // given
            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox(_accessToken));

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

            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox(_accessToken));

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
            Assert.That(result.Subscription.Links.Mandate, Is.EqualTo(_mandate.Id));
        }

        [Test]
        public async Task UpdatesSubscription()
        {
            // given
            var request = new UpdateSubscriptionRequest
            {
                Id = "SB0000G9PZP814",
                Amount = 456,
                Name = "Updated subscription name"
            };

            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.UpdateAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(request));
            Assert.That(result.Subscription.Amount, Is.EqualTo(request.Amount));
            Assert.That(result.Subscription.AppFee, Is.EqualTo(0));
            Assert.That(result.Subscription.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Subscription.Name, Is.EqualTo(request.Name));
            Assert.That(result.Subscription.PaymentReference, Is.Null);
        }

        [Test]
        public async Task CancelsSubscription()
        {
            // given
            var request = new CancelSubscriptionRequest
            {
                Id = "SB0000GN09Z7CY"
            };

            var subject = new SubscriptionsClient(ClientConfiguration.ForSandbox(_accessToken));

            // when
            var result = await subject.CancelAsync(request);

            // then
            Assert.That(result.Subscription.Id, Is.EqualTo(request.Id));
            Assert.That(result.Subscription.Status, Is.EqualTo("cancelled"));
        }
    }
}