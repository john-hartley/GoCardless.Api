using GoCardless.Api.Core;
using GoCardless.Api.Core.Paging;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public interface ISubscriptionsClient
    {
        Task<PagedResponse<Subscription>> AllAsync();
        Task<PagedResponse<Subscription>> AllAsync(AllSubscriptionsRequest request);
        Task<Response<Subscription>> CancelAsync(CancelSubscriptionRequest request);
        Task<Response<Subscription>> CreateAsync(CreateSubscriptionRequest request);
        Task<Response<Subscription>> ForIdAsync(string subscriptionId);
        Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionRequest request);
    }
}