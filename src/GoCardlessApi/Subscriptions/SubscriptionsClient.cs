using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Subscriptions
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AllSubscriptionsResponse> AllAsync()
        {
            return GetAsync<AllSubscriptionsResponse>("subscriptions");
        }

        public Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateSubscriptionRequest, CreateSubscriptionResponse>(
                new { subscriptions = request },
                idempotencyKey,
                new string[] { "subscriptions" }
            );
        }

        public Task<SubscriptionResponse> ForIdAsync(string subscriptionId)
        {
            return GetAsync<SubscriptionResponse>("subscriptions", subscriptionId);
        }

        public Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request)
        {
            return PutAsync<UpdateSubscriptionRequest, UpdateSubscriptionResponse>(
                new { subscriptions = request },
                "subscriptions", 
                request.Id
            );
        }

        public Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request)
        {
            return PostAsync<CancelSubscriptionRequest, CancelSubscriptionResponse>(
                request,
                new string[] { "subscriptions", request.Id, "actions", "cancel" }
            );
        }
    }
}