using GoCardless.Api.Core.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.PayoutItems
{
    public interface IPayoutItemsClient
    {
        IPagerBuilder<GetPayoutItemsRequest, PayoutItem> BuildPager();
        Task<PagedResponse<PayoutItem>> GetPageAsync(GetPayoutItemsRequest request);
    }
}