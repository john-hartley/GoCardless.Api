﻿using Flurl.Http;
using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ApiClient, ISubscriptionsClient
    {
        private readonly IApiClient _apiClient;

        public SubscriptionsClient(IApiClient apiClient, ClientConfiguration configuration) : base(configuration)
        {
            _apiClient = apiClient;
        }

        public IPagerBuilder<GetSubscriptionsRequest, Subscription> BuildPager()
        {
            return new Pager<GetSubscriptionsRequest, Subscription>(GetPageAsync);
        }

        public async Task<Response<Subscription>> CancelAsync(CancelSubscriptionRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.Id));
            }

            return await _apiClient.PostAsync<Response<Subscription>>(
                "subscriptions",
                new { subscriptions = options },
                request =>
                {
                    request.AppendPathSegment($"subscriptions/{options.Id}/actions/cancel");
                });
        }

        public async Task<Response<Subscription>> CreateAsync(CreateSubscriptionRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<Subscription>>(
                "subscriptions",
                new { subscriptions = options },
                request =>
                {
                    request
                        .AppendPathSegment("subscriptions")
                        .WithHeader("Idempotency-Key", options.IdempotencyKey);
                });
        }

        public async Task<Response<Subscription>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return await _apiClient.GetAsync<Response<Subscription>>(request =>
            {
                request.AppendPathSegment($"subscriptions/{id}");
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync()
        {
            return await _apiClient.GetAsync<PagedResponse<Subscription>>(request =>
            {
                request.AppendPathSegment("subscriptions");
            });
        }

        public async Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.GetAsync<PagedResponse<Subscription>>(request =>
            {
                request
                    .AppendPathSegment("subscriptions")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }

        public Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<Response<Subscription>>(
                $"subscriptions/{request.Id}",
                new { subscriptions = request }
            );
        }
    }
}