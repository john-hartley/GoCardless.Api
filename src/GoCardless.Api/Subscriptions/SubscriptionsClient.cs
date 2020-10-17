﻿using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly IApiClient _apiClient;

        public SubscriptionsClient(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public SubscriptionsClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<Subscription>> CancelAsync(CancelSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"subscriptions/{options.Id}/actions/cancel")
                    .PostJsonAsync(new { subscriptions = options })
                    .ReceiveJson<Response<Subscription>>();
            });
        }

        public async Task<Response<Subscription>> CreateAsync(CreateSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.IdempotentRequestAsync(
                options.IdempotencyKey,
                request =>
                {
                    return request
                        .AppendPathSegment("subscriptions")
                        .PostJsonAsync(new { subscriptions = options })
                        .ReceiveJson<Response<Subscription>>();
                });
        }

        public async Task<Response<Subscription>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment($"subscriptions/{id}")
                    .GetJsonAsync<Response<Subscription>>();
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync()
        {
            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("subscriptions")
                    .GetJsonAsync<PagedResponse<Subscription>>();
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("subscriptions")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<Subscription>>();
            });
        }

        public IPager<GetSubscriptionsOptions, Subscription> PageFrom(GetSubscriptionsOptions options)
        {
            return new Pager<GetSubscriptionsOptions, Subscription>(GetPageAsync, options);
        }

        public async Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PutAsync<Response<Subscription>>(
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}");
                },
                new { subscriptions = options });
        }
    }
}