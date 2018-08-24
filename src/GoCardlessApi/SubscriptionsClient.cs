using System;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class SubscriptionsClient : ApiClientBase, ISubscriptionsClient
    {
        private readonly ClientConfiguration _configuration;

        public SubscriptionsClient(ClientConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

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
                "subscriptions"
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
                "subscriptions",
                request.Id,
                "actions",
                "cancel"
            );
        }
    }
}