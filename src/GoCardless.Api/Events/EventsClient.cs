using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly ApiClient _apiClient;

        public EventsClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<Event>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"events/{id}")
                    .GetJsonAsync<Response<Event>>();
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("events")
                    .GetJsonAsync<PagedResponse<Event>>();
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync(GetEventsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("events")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Event>>();
            });
        }

        public IPager<GetEventsOptions, Event> PageFrom(GetEventsOptions options)
        {
            return new Pager<GetEventsOptions, Event>(GetPageAsync, options);
        }
    }
}