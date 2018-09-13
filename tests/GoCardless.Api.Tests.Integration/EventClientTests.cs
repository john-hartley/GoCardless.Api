using GoCardless.Api.Events;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class EventClientTests : IntegrationTest
    {
        [Test]
        public async Task ReturnsEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            // when
            var result = (await subject.AllAsync()).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsMandateEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                ResourceType = "mandates"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Mandate, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsPaymentEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                ResourceType = "payments"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Payment, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsPayoutEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                ResourceType = "payouts"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Payout, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsRefundEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                ResourceType = "refunds"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Refund, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsSubscriptionEvents()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                ResourceType = "subscriptions"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Subscription, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var firstPageRequest = new AllEventsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.AllAsync(firstPageRequest);

            var secondPageRequest = new AllEventsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
            };

            var secondPageResult = await subject.AllAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(firstPageResult.Events.Count(), Is.EqualTo(firstPageRequest.Limit));

            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(secondPageResult.Events.Count(), Is.EqualTo(secondPageRequest.Limit));
        }

        [Test]
        public async Task MapsParentEvent()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                Action = "paid_out",
                ResourceType = "payments"
            };

            // when
            var result = (await subject.AllAsync(request)).Events.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Action, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Details, Is.Not.Null);
            Assert.That(result[0].Details.Cause, Is.Not.Null);
            Assert.That(result[0].Details.Description, Is.Not.Null);
            Assert.That(result[0].Details.Origin, Is.Not.Null);
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.ParentEvent, Is.Not.Null);
            Assert.That(result[0].Links.Payment, Is.Not.Null);
            Assert.That(result[0].Links.Payout, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].ResourceType, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualEvent()
        {
            // given
            var subject = new EventsClient(_clientConfiguration);

            var request = new AllEventsRequest
            {
                Action = "paid_out",
                ResourceType = "payments"
            };
            
            var events = (await subject.AllAsync(request)).Events.ToList();
            var @event = events.First();

            // when
            var result = await subject.ForIdAsync(@event.Id);
            var actual = result.Event;

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
    }
}