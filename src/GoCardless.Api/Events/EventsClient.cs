using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : ApiClient, IEventsClient
    {
        private readonly IApiClient _apiClient;

        public EventsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetEventsRequest, Event> BuildPager()
        {
            return new Pager<GetEventsRequest, Event>(GetPageAsync);
        }

        public async Task<Response<Event>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Event>>(request =>
            {
                request.AppendPathSegment($"events/{id}");
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Event>>(request =>
            {
                request.AppendPathSegment("events");
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync(GetEventsRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Event>>(request =>
            {
                request
                    .AppendPathSegment("events")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }
    }
}