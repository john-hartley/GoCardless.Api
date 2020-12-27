using GoCardlessApi.Events;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class EventClientTests : IntegrationTest
    {
        private IEventsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new EventsClient(_configuration);
        }

        [Test]
        public async Task returns_events()
        {
            // given
            // when
            var results = (await _subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task returns_creditor_events()
        {
            // given
            var options = new GetEventsOptions
            {
                Action = Actions.Creditor.Updated,
                ResourceType = ResourceType.Creditors
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.EqualTo(Actions.Creditor.Updated));
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.EqualTo(Causes.CreditorUpdated));
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Details.Property, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Creditor, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Creditors));
        }

        [Test]
        public async Task returns_mandate_events()
        {
            // given
            var options = new GetEventsOptions
            {
                ResourceType = ResourceType.Mandates
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Mandate, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Mandates));
        }

        [Test]
        public async Task returns_payment_events()
        {
            // given
            var options = new GetEventsOptions
            {
                ResourceType = ResourceType.Payments
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Payment, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Payments));
        }

        [Test]
        public async Task returns_payout_events()
        {
            // given
            var options = new GetEventsOptions
            {
                ResourceType = ResourceType.Payouts
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Payout, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Payouts));
        }

        [Test]
        public async Task returns_refund_events()
        {
            // given
            var options = new GetEventsOptions
            {
                ResourceType = ResourceType.Refunds
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Refund, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Refunds));
        }

        [Test]
        public async Task returns_subscription_events()
        {
            // given
            var options = new GetEventsOptions
            {
                ResourceType = ResourceType.Subscriptions
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(results.Any(), Is.True);
            Assert.That(results[0], Is.Not.Null);
            Assert.That(results[0].Id, Is.Not.Null);
            Assert.That(results[0].Action, Is.Not.Null);
            Assert.That(results[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(results[0].Details, Is.Not.Null);
            Assert.That(results[0].Details.Cause, Is.Not.Null);
            Assert.That(results[0].Details.Description, Is.Not.Null);
            Assert.That(results[0].Details.Origin, Is.Not.Null);
            Assert.That(results[0].Links, Is.Not.Null);
            Assert.That(results[0].Links.Subscription, Is.Not.Null);
            Assert.That(results[0].Metadata, Is.Not.Null);
            Assert.That(results[0].ResourceType, Is.EqualTo(ResourceType.Subscriptions));
        }

        [Test]
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetEventsOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetEventsOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
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
        public async Task maps_parent_event()
        {
            // given
            var options = new GetEventsOptions
            {
                Action = Actions.Payment.PaidOut,
                ResourceType = ResourceType.Payments
            };

            // when
            var result = (await _subject.GetPageAsync(options)).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.EqualTo(Actions.Payment.PaidOut));
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.EqualTo(Causes.PaymentPaidOut));
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.EqualTo(Origin.GoCardless));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.ParentEvent, Is.Not.Null);
            Assert.That(result[0].Links.Payment, Is.Not.Null);
            Assert.That(result[0].Links.Payout, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.EqualTo(ResourceType.Payments));
        }

        [Test]
        public async Task maps_bank_account_id_and_currency()
        {
            // given
            var options = new GetEventsOptions
            {
                Action = Actions.Creditor.NewPayoutCurrencyAdded,
                ResourceType = ResourceType.Creditors
            };

            // when
            var results = (await _subject.GetPageAsync(options)).Items.ToList();
            var actual = results.FirstOrDefault(x => x.Details.Currency != null);

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Action, Is.EqualTo(Actions.Creditor.NewPayoutCurrencyAdded));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Details, Is.Not.Null);
            Assert.That(actual.Details.BankAccountId, Is.Not.Null);
            Assert.That(actual.Details.Cause, Is.EqualTo(Causes.NewPayoutCurrencyAdded));
            Assert.That(actual.Details.Description, Is.Not.Null);
            Assert.That(actual.Details.Origin, Is.EqualTo(Origin.GoCardless));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Creditor, Is.Not.Null);
            Assert.That(actual.Metadata, Is.Not.Null);
            Assert.That(actual.ResourceType, Is.EqualTo(ResourceType.Creditors));
        }

        [Test]
        public async Task returns_event()
        {
            // given
            var options = new GetEventsOptions
            {
                Action = Actions.Payment.PaidOut,
                ResourceType = ResourceType.Payments
            };
            
            var events = (await _subject.GetPageAsync(options)).Items.ToList();
            var @event = events.First();

            // when
            var result = await _subject.ForIdAsync(@event.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(@event.Id));
            Assert.That(actual.Action, Is.Not.Null.And.EqualTo(@event.Action));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(@event.CreatedAt));
            Assert.That(actual.Details, Is.Not.Null);
            Assert.That(actual.Details.Cause, Is.Not.Null.And.EqualTo(@event.Details.Cause));
            Assert.That(actual.Details.Description, Is.Not.Null.And.EqualTo(@event.Details.Description));
            Assert.That(actual.Details.Origin, Is.Not.Null.And.EqualTo(@event.Details.Origin));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.ParentEvent, Is.Not.Null.And.EqualTo(@event.Links.ParentEvent));
            Assert.That(actual.Links.Payment, Is.Not.Null.And.EqualTo(@event.Links.Payment));
            Assert.That(actual.Links.Payout, Is.Not.Null.And.EqualTo(@event.Links.Payout));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(@event.Metadata));
            Assert.That(actual.ResourceType, Is.Not.Null.And.EqualTo(@event.ResourceType));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_events()
        {
            // given
            // given
            var items = (await _subject.GetPageAsync()).Items;
            var first = items.First();
            var last = items.Last();

            var options = new GetEventsOptions
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