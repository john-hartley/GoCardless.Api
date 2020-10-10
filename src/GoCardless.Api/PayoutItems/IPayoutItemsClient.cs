using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient : IPageable<GetPayoutItemsOptions, PayoutItem>
    {
        Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsOptions options);
    }
}