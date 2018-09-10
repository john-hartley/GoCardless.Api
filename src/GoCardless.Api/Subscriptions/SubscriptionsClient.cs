﻿using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllSubscriptionsResponse> AllAsync()
        {
            return GetAsync<AllSubscriptionsResponse>("subscriptions");
        }

        public Task<AllSubscriptionsResponse> AllAsync(AllSubscriptionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<AllSubscriptionsResponse>("subscriptions", request.ToReadOnlyDictionary());
        }

        public Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<CancelSubscriptionResponse>(
                $"subscriptions/{request.Id}/actions/cancel",
                new { subscriptions = request }
            );
        }

        public Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<CreateSubscriptionResponse>(
                "subscriptions",
                new { subscriptions = request },
                request.IdempotencyKey
            );
        }

        public Task<SubscriptionResponse> ForIdAsync(string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(subscriptionId));
            }

            return GetAsync<SubscriptionResponse>($"subscriptions/{subscriptionId}");
        }

        public Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<UpdateSubscriptionResponse>(
                $"subscriptions/{request.Id}",
                new { subscriptions = request }
            );
        }
    }
}