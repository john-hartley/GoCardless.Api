using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : ApiClientBase, IEventsClient
    {
        public EventsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllEventsResponse> AllAsync()
        {
            return GetAsync<AllEventsResponse>("events");
        }

        public Task<AllEventsResponse> AllAsync(AllEventsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllEventsResponse>(
                "events",
                request.ToReadOnlyDictionary()
            );
        }

        public Task<EventResponse> ForIdAsync(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(eventId));
            }

            return GetAsync<EventResponse>($"events/{eventId}");
        }
    }
}