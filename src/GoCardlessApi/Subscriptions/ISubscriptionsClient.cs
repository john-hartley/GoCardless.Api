using System.Threading.Tasks;

namespace GoCardlessApi.Subscriptions
{
    public interface ISubscriptionsClient
    {
        Task<AllSubscriptionsResponse> AllAsync();
        Task<CreateSubscriptionResponse> CreateAsync(CreateSubscriptionRequest request);
        Task<SubscriptionResponse> ForIdAsync(string subscriptionId);
        Task<UpdateSubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request);
        Task<CancelSubscriptionResponse> CancelAsync(CancelSubscriptionRequest request);
    }
}