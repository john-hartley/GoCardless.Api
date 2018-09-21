using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public interface ISubscriptionsClient
    {
        IPagerBuilder<GetSubscriptionsRequest, Subscription> BuildPager();
        Task<Response<Subscription>> CancelAsync(CancelSubscriptionRequest request);
        Task<Response<Subscription>> CreateAsync(CreateSubscriptionRequest request);
        Task<Response<Subscription>> ForIdAsync(string id);
        Task<PagedResponse<Subscription>> GetPageAsync();
        Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsRequest request);
        Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionRequest request);
    }
}