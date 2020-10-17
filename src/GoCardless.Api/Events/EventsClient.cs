﻿using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly IApiClient _apiClient;

        public EventsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public EventsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<Event>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync<Response<Event>>(request =>
            {
                request.AppendPathSegment($"events/{id}");
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync()
        {
            return await _apiClient.RequestAsync<PagedResponse<Event>>(request =>
            {
                request.AppendPathSegment("events");
            });
        }

        public async Task<PagedResponse<Event>> GetPageAsync(GetEventsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync<PagedResponse<Event>>(request =>
            {
                request
                    .AppendPathSegment("events")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public IPager<GetEventsOptions, Event> PageFrom(GetEventsOptions options)
        {
            return new Pager<GetEventsOptions, Event>(GetPageAsync, options);
        }
    }
}