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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            // when
            var result = (await subject.GetPageAsync()).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                ResourceType = ResourceType.Mandates
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                ResourceType = ResourceType.Payments
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                ResourceType = ResourceType.Payouts
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                ResourceType = ResourceType.Refunds
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                ResourceType = ResourceType.Subscriptions
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var firstPageRequest = new GetEventsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetEventsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
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
        public async Task MapsParentEvent()
        {
            // given
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                Action = Actions.Payment.PaidOut,
                ResourceType = ResourceType.Payments
            };

            // when
            var result = (await subject.GetPageAsync(request)).Items.ToList();

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
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);

            var request = new GetEventsRequest
            {
                Action = Actions.Payment.PaidOut,
                ResourceType = ResourceType.Payments
            };
            
            var events = (await subject.GetPageAsync(request)).Items.ToList();
            var @event = events.First();

            // when
            var result = await subject.ForIdAsync(@event.Id);
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

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task PagesThroughEvents()
        {
            // given
            var subject = new EventsClient(_apiClient, _apiClient.Configuration);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetEventsRequest
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1))
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