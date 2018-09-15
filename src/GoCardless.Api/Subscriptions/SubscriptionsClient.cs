using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<PagedResponse<Subscription>> AllAsync()
        {
            return GetAsync<PagedResponse<Subscription>>("subscriptions");
        }

        public Task<PagedResponse<Subscription>> AllAsync(AllSubscriptionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Subscription>>("subscriptions", request.ToReadOnlyDictionary());
        }

        public Task<SubscriptionResponse> CancelAsync(CancelSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<SubscriptionResponse>(
                $"subscriptions/{request.Id}/actions/cancel",
                new { subscriptions = request }
            );
        }

        public Task<SubscriptionResponse> CreateAsync(CreateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<SubscriptionResponse>(
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

        public Task<SubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PutAsync<SubscriptionResponse>(
                $"subscriptions/{request.Id}",
                new { subscriptions = request }
            );
        }
    }
}