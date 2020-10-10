using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public interface ISubscriptionsClient
    {
        IPagerBuilder<GetSubscriptionsOptions, Subscription> BuildPager();
        Task<Response<Subscription>> CancelAsync(CancelSubscriptionOptions options);
        Task<Response<Subscription>> CreateAsync(CreateSubscriptionOptions options);
        Task<Response<Subscription>> ForIdAsync(string id);
        Task<PagedResponse<Subscription>> GetPageAsync();
        Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsOptions options);
        Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionOptions options);
    }
}