using GoCardless.Api.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Subscriptions
{
    public interface ISubscriptionsClient : IPageable<GetSubscriptionsOptions, Subscription>
    {
        Task<Response<Subscription>> CancelAsync(CancelSubscriptionOptions options);
        Task<Response<Subscription>> CreateAsync(CreateSubscriptionOptions options);
        Task<Response<Subscription>> ForIdAsync(string id);
        Task<PagedResponse<Subscription>> GetPageAsync();
        Task<PagedResponse<Subscription>> GetPageAsync(GetSubscriptionsOptions options);
        Task<Response<Subscription>> UpdateAsync(UpdateSubscriptionOptions options);
    }
}