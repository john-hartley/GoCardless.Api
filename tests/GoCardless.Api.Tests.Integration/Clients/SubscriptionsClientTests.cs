using GoCardlessApi.Mandates;
using GoCardlessApi.Subscriptions;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class SubscriptionsClientTests : IntegrationTest
    {
        private ISubscriptionsClient _subject;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customerBankAccount);
        }

        [SetUp]
        public void Setup()
        {
            _subject = new SubscriptionsClient(_configuration);
        }

        [Test]
        public async Task creates_subscription_specifying_number_of_payments()
        {
            // given
            var options = new CreateSubscriptionOptions
            {
                Amount = 123,
                Count = 5,
                Currency = "GBP",
                Interval = 1,
                IntervalUnit = IntervalUnit.Weekly,
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = Metadata.Initial,
                Name = "Test subscription",
                PaymentReference = "PR123456",
                StartDate = DateTime.Now.AddMonths(1)
            };

            _subject = new SubscriptionsClient(_configuration);

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.Not.Empty);
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.Count, Is.EqualTo(options.Count));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.DayOfMonth, Is.Null);
            Assert.That(result.Item.Interval, Is.EqualTo(options.Interval));
            Assert.That(result.Item.IntervalUnit, Is.EqualTo(options.IntervalUnit));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.EqualTo(options.Links.Mandate));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Month, Is.Null);
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(options.PaymentReference));
            Assert.That(result.Item.StartDate.Date, Is.EqualTo(options.StartDate.Value.Date));
            Assert.That(result.Item.Status, Is.EqualTo(SubscriptionStatus.Active));
            Assert.That(result.Item.UpcomingPayments.Count(), Is.EqualTo(options.Count));
        }

        [Test]
        public async Task creates_subscription_using_month_and_day_of_month()
        {
            // given
            var startDate = _mandate.NextPossibleChargeDate.Value;
            var options = new CreateSubscriptionOptions
            {
                Amount = 123,
                Currency = "GBP",
                DayOfMonth = DateTime.Now.Day,
                Interval = 1,
                IntervalUnit = IntervalUnit.Yearly,
                Links = new SubscriptionLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = Metadata.Initial,
                Month = Month.NameFrom(startDate),
                Name = "Test subscription",
                PaymentReference = "PR123456",
                StartDate = startDate
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.Not.Empty);
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.DayOfMonth, Is.EqualTo(options.DayOfMonth));
            Assert.That(result.Item.Interval, Is.EqualTo(options.Interval));
            Assert.That(result.Item.IntervalUnit, Is.EqualTo(options.IntervalUnit));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.EqualTo(options.Links.Mandate));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Month, Is.EqualTo(options.Month));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(options.PaymentReference));
            Assert.That(result.Item.StartDate.Date, Is.EqualTo(options.StartDate.Value.Date));
            Assert.That(result.Item.Status, Is.EqualTo(SubscriptionStatus.Active));
        }

        [Test, Explicit("Needs a merchant account to be setup, an OAuth access token to have been exchanged, and a mandate setup via a redirect flow.")]
        [Category(TestCategory.NeedsMerchantAccount)]
        public async Task creates_subscription_for_merchant()
        {
            // given
            var accessToken = Environment.GetEnvironmentVariable("GoCardlessMerchantAccessToken");
            var configuration = GoCardlessConfiguration.ForSandbox(accessToken, false);
            var mandatesClient = new MandatesClient(configuration);
            var mandate = (await mandatesClient.GetPageAsync()).Items.First();

            var options = new CreateSubscriptionOptions
            {
                Amount = 123,
                AppFee = 12,
                Count = 5,
                Currency = "GBP",
                Interval = 1,
                IntervalUnit = IntervalUnit.Weekly,
                Links = new SubscriptionLinks
                {
                    Mandate = mandate.Id
                },
                Metadata = Metadata.Initial,
                Name = "Test subscription",
                StartDate = DateTime.Now.AddMonths(1)
            };

            _subject = new SubscriptionsClient(configuration);

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.Not.Empty);
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.AppFee, Is.EqualTo(options.AppFee));
            Assert.That(result.Item.Count, Is.EqualTo(options.Count));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.DayOfMonth, Is.EqualTo(options.DayOfMonth));
            Assert.That(result.Item.Interval, Is.EqualTo(options.Interval));
            Assert.That(result.Item.IntervalUnit, Is.EqualTo(options.IntervalUnit));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.EqualTo(options.Links.Mandate));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Month, Is.EqualTo(options.Month));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(options.PaymentReference));
            Assert.That(result.Item.StartDate.Date, Is.EqualTo(options.StartDate.Value.Date));
            Assert.That(result.Item.Status, Is.EqualTo(SubscriptionStatus.Active));
            Assert.That(result.Item.UpcomingPayments.Count(), Is.EqualTo(options.Count));
        }

        [Test]
        public async Task returns_subscriptions()
        {
            // given
            // when
            var result = await _subject.GetPageAsync();
            var actual = result.Items.ToList();

            // then
            Assert.That(actual.Any(), Is.True);
            Assert.That(result.Meta.Limit, Is.Not.EqualTo(0));
            Assert.That(actual.Count, Is.LessThanOrEqualTo(result.Meta.Limit));
            Assert.That(actual[0], Is.Not.Null);
            Assert.That(actual[0].Id, Is.Not.Null);
            Assert.That(actual[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(actual[0].AppFee, Is.Null);
            Assert.That(actual[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual[0].Currency, Is.Not.Null);
            Assert.That(actual[0].Interval, Is.Not.Null);
            Assert.That(actual[0].IntervalUnit, Is.Not.Null);
            Assert.That(actual[0].Links, Is.Not.Null);
            Assert.That(actual[0].Links.Mandate, Is.Not.Null);
            Assert.That(actual[0].Metadata, Is.Not.Null);
            Assert.That(actual[0].Name, Is.Not.Null);
            Assert.That(actual[0].PaymentReference, Is.Not.Null);
            Assert.That(actual[0].StartDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(actual[0].Status, Is.Not.Null);
            Assert.That(actual[0].UpcomingPayments, Is.Not.Null);
            Assert.That(actual[0].UpcomingPayments.Any(), Is.True);
        }

        [Test]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetSubscriptionsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetSubscriptionsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await _subject.GetPageAsync(secondPageOptions);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task returns_subscription()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            // when
            var result = await _subject.ForIdAsync(subscription.Id);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(subscription.Id));
            Assert.That(result.Item.Amount, Is.Not.Null.And.EqualTo(subscription.Amount));
            Assert.That(result.Item.Count, Is.Not.Null.And.EqualTo(subscription.Count));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Currency, Is.Not.Null.And.EqualTo(subscription.Currency));
            Assert.That(result.Item.Interval, Is.Not.Null.And.EqualTo(subscription.Interval));
            Assert.That(result.Item.IntervalUnit, Is.Not.Null.And.EqualTo(subscription.IntervalUnit));
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.Not.Null.And.EqualTo(subscription.Links.Mandate));
            Assert.That(result.Item.Metadata, Is.Not.Null.And.EqualTo(subscription.Metadata));
            Assert.That(result.Item.Name, Is.Not.Null.And.EqualTo(subscription.Name));
            Assert.That(result.Item.PaymentReference, Is.Not.Null.And.EqualTo(subscription.PaymentReference));
            Assert.That(result.Item.StartDate, Is.Not.Null.And.EqualTo(subscription.StartDate));
            Assert.That(result.Item.Status, Is.Not.Null.And.EqualTo(subscription.Status));
            Assert.That(result.Item.UpcomingPayments, Is.Not.Null);
            Assert.That(result.Item.UpcomingPayments.Count(), Is.EqualTo(subscription.UpcomingPayments.Count()));
        }

        [Test]
        public async Task updates_subscription_preserving_metadata()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var options = new UpdateSubscriptionOptions
            {
                Id = subscription.Id,
                Amount = 456,
                Name = "Updated subscription name",
                PaymentReference = "PR456789"
            };

            // when
            var result = await _subject.UpdateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(options.Id));
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.Metadata, Is.EqualTo(subscription.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(options.PaymentReference));
        }

        [Test]
        public async Task updates_subscription_replacing_metadata()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var options = new UpdateSubscriptionOptions
            {
                Id = subscription.Id,
                Amount = 456,
                Metadata = Metadata.Updated,
                Name = "Updated subscription name",
                PaymentReference = "PR456789"
            };

            // when
            var result = await _subject.UpdateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(options.Id));
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentReference, Is.EqualTo(options.PaymentReference));
        }

        [Test, Explicit("Needs a merchant account to be setup, an OAuth access token to have been exchanged, and a mandate setup via a redirect flow.")]
        [Category(TestCategory.NeedsMerchantAccount)]
        public async Task updates_subscription_for_merchant()
        {
            // given
            var accessToken = Environment.GetEnvironmentVariable("GoCardlessMerchantAccessToken");
            var configuration = GoCardlessConfiguration.ForSandbox(accessToken, false);
            var resourceFactory = new ResourceFactory(configuration);

            var mandatesClient = new MandatesClient(configuration);
            var mandate = (await mandatesClient.GetPageAsync()).Items.First();
            var subscription = await resourceFactory.CreateSubscriptionFor(mandate, paymentReference: null);

            var options = new UpdateSubscriptionOptions
            {
                Id = subscription.Id,
                Amount = 456,
                AppFee = 34,
                Metadata = Metadata.Updated,
                Name = "Updated subscription name"
            };

            _subject = new SubscriptionsClient(configuration);

            // when
            var result = await _subject.UpdateAsync(options);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(options.Id));
            Assert.That(result.Item.Amount, Is.EqualTo(options.Amount));
            Assert.That(result.Item.AppFee, Is.EqualTo(options.AppFee));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
        }

        [Test]
        public async Task cancels_subscription()
        {
            // given
            var subscription = await _resourceFactory.CreateSubscriptionFor(_mandate);

            var options = new CancelSubscriptionOptions
            {
                Id = subscription.Id,
                Metadata = Metadata.Updated
            };

            // when
            var result = await _subject.CancelAsync(options);

            // then
            Assert.That(result.Item.Id, Is.EqualTo(options.Id));
            Assert.That(result.Item.Status, Is.EqualTo(SubscriptionStatus.Cancelled));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_subscriptions()
        {
            // given
            var items = (await _subject.GetPageAsync()).Items;
            var first = items.First();
            var last = items.Last();

            var options = new GetSubscriptionsOptions
            {
                After = first.Id,
                CreatedGreaterThan = last.CreatedAt.AddDays(-1)
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result.Any(x => x.Id == first.Id), Is.False);
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}