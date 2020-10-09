using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : ApiClient, IEventsClient
    {
        public EventsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetEventsRequest, Event> BuildPager()
        {
            return new Pager<GetEventsRequest, Event>(GetPageAsync);
        }

        public Task<Response<Event>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<Event>>($"events/{id}");
        }

        public Task<PagedResponse<Event>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Event>>("events");
        }

        public Task<PagedResponse<Event>> GetPageAsync(GetEventsRequest request)
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
    }
}