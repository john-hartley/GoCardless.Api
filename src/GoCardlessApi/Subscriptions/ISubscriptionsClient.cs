using System.Threading.Tasks;

namespace GoCardlessApi.Subscriptions
{
    public interface ISubscriptionsClient
    {
        Task<AllSubscriptionsResponse> AllAsync();
        Task<AllSubscriptionsResponse> AllAsync(AllSubscriptionsRequest request);
        Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request);
        Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request);
        Task<SubscriptionResponse> ForIdAsync(string subscriptionId);
        Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request);
    }
}