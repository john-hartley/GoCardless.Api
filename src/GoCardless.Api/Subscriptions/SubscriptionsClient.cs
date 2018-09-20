using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration) { }

        public IPagerBuilder<GetSubscriptionsRequest, Subscription> BuildPager()
        {
            return new Pager<GetSubscriptionsRequest, Subscription>(GetPageAsync);
        }

        public Task<Response<Subscription>> CancelAsync(CancelSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.Id));
            }

            return PostAsync<Response<Subscription>>(
                $"subscriptions/{request.Id}/actions/cancel",
                new { subscriptions = request }
            );
        }

        public Task<Response<Subscription>> CreateAsync(CreateSubscriptionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<Subscription>>(
                "subscriptions",
                new { subscriptions = request },
                request.IdempotencyKey
            );
        }

        public Task<Response<Subscription>> ForIdAsync(string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(subscriptionId));
            }

            return GetAsync<Response<Subscription>>($"subscriptions/{subscriptionId}");
        }

        public Task<PagedResponse<Subscription>> GetPageAsync()
        {
            return GetAsync<PagedResponse<Subscription>>("subscriptions");
        }

        public Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetAsync<PagedResponse<Subscription>>("subscriptions", request.ToReadOnlyDictionary());
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