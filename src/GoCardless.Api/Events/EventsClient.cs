using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Paging;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : ApiClientBase, IEventsClient
    {
        public EventsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Event>> AllAsync()
        {
            return GetAsync<PagedResponse<Event>>("events");
        }

        public Task<PagedResponse<Event>> AllAsync(AllEventsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Event>>(
                "events",
                request.ToReadOnlyDictionary()
            );
        }

        public Task<Response<Event>> ForIdAsync(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(eventId));
            }

            return GetAsync<Response<Event>>($"events/{eventId}");
        }
    }
}