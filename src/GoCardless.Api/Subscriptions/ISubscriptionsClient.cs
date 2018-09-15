using GoCardless.Api.Core;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public interface ISubscriptionsClient
    {
        Task<PagedResponse<Subscription>> AllAsync();
        Task<PagedResponse<Subscription>> AllAsync(AllSubscriptionsRequest request);
        Task<SubscriptionResponse> CancelAsync(CancelSubscriptionRequest request);
        Task<SubscriptionResponse> CreateAsync(CreateSubscriptionRequest request);
        Task<SubscriptionResponse> ForIdAsync(string subscriptionId);
        Task<SubscriptionResponse> UpdateAsync(UpdateSubscriptionRequest request);
    }
}